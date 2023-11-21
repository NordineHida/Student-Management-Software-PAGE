using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Dao des notes pour la base de donnée oracle
    /// </summary>
    /// <author>Nordine</author>
    public class NoteDAOOracle : INoteDAO
    {
        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateNote(Note? note)
        {
            bool ajoutReussi = false;
            if (note != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    // On crée la requête SQL
                    string requete = String.Format("INSERT INTO Note(idNote,categorie,datePublication,nature,commentaire,apogeeEtudiant)" +
                        "VALUES(0, '{0}', TO_DATE('{1}', 'YYYY-MM-DD'), '{2}', '{3}', '{4}')", getCatNote(note), note.DatePublication.Date.ToString("yyyy-MM-dd"), getNatNote(note), note.Commentaire, note.ApogeeEtudiant);


                    //On execute la requete
                    OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion);


                    //On verifie que la ligne est bien inséré, si oui on passe le bool à true
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        ajoutReussi = true;
                    }
                }
                // Gestion des exceptions
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    try
                    {
                        if (con != null)
                        {
                            con.Close();
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return ajoutReussi;
        }

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo/Nordine</author>
        public bool DeleteNote(Note note)
        {
            bool suppressionReussie = false;
            if (note != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    // On crée la requête SQL
                    string requete = $"DELETE FROM NOTE WHERE idNote={note.IdNote}";


                    //On execute la requete
                    OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion);


                    //On verifie que la ligne est bien inséré, si oui on passe le bool à true
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        suppressionReussie = true;
                    }
                }
                // Gestion des exceptions
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    try
                    {
                        if (con != null)
                        {
                            con.Close();
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return suppressionReussie;
        }

        /// <summary>
        /// Renvoie toutes les notes d'un étudiant 
        /// </summary>
        /// <returns>la liste de notes/returns>
        /// <author>Laszlo</author>
        public IEnumerable<Note> GetAllNotesByApogee(int apogeeEtudiant)
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste de note à renvoyer
            List<Note> notes = new List<Note>();

            try
            {
                string requete = String.Format("SELECT idNote,categorie,datePublication,nature,commentaire,apogeeEtudiant FROM Note WHERE apogeeEtudiant={0}", apogeeEtudiant);
                // Création d'une commande Oracle pour récuperer l'ensemble des éléments des notes
                OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    //Récupération(lecture) de tous les éléments d'une note en bdd
                    int idNote = reader.GetInt32(reader.GetOrdinal("idNote"));
                    string categorieStr = reader.GetString(reader.GetOrdinal("categorie"));
                    DateTime datePublication = reader.GetDateTime(reader.GetOrdinal("datePublication"));
                    string natureStr = reader.GetString(reader.GetOrdinal("nature"));
                    string commentaire = reader.GetString(reader.GetOrdinal("commentaire"));

                    CATEGORIE categorie=CATEGORIE.AUTRE;
                    switch (categorieStr)
                    {
                        case "Absenteisme":
                            categorie = CATEGORIE.ABSENTEISME;
                            break;
                        case "Medical":
                            categorie = CATEGORIE.MEDICAL;
                            break;
                        case "Personnel":
                            categorie = CATEGORIE.PERSONNEL;
                            break;
                        case "Resultats":
                            categorie = CATEGORIE.RESULTATS;
                            break;
                        case "Autre":
                            categorie = CATEGORIE.AUTRE;
                            break;
                    }

                    NATURE nature = NATURE.AUTRE;
                    switch (natureStr)
                    {
                        case "Nature":
                            nature = NATURE.LETTRE;
                            break;
                        case "Rdv":
                            nature = NATURE.RDV;
                            break;
                        case "Mail":
                            nature = NATURE.MAIL;
                            break;
                        case "Appel":
                            nature = NATURE.APPEL;
                            break;
                        case "Autre":
                            nature = NATURE.AUTRE;
                            break;
                    }

                    // Création de l'objet Note en utilisant les variables
                    Note note = new Note(categorie, datePublication, nature, commentaire, apogeeEtudiant);
                    note.IdNote= idNote;

                    notes.Add(note);
                }
            }
            // Gestion des exceptions
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //On ferme la connexion
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return notes;

        }


        /// <summary>
        /// Modifie une note de la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine</author>
        public bool UpdateNote(Note note)
        {
            bool modificationReussie = false;
            if (note != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();
                try
                {
                    // On crée la requête SQL
                    string requete = String.Format("UPDATE Note " +
                                                   "SET categorie = '{0}', " +
                                                   "datePublication = TO_DATE('{1}', 'YYYY-MM-DD'), " +
                                                   "nature = '{2}', " +
                                                   "commentaire = '{3}', " +
                                                   "apogeeEtudiant = '{4}' " +
                                                   "WHERE idNote = {5}",
                                                   getCatNote(note), note.DatePublication.Date.ToString("yyyy-MM-dd"), getNatNote(note), note.Commentaire, note.ApogeeEtudiant, note.IdNote);
                    //On execute la requete
                    OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion);
                    //On vérifie que la ligne est bien modifiée, si oui on passe le bool à true
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        modificationReussie = true;
                    }
                }
                // Gestion des exceptions
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    try
                    {
                        if (con != null)
                        {
                            con.Close();
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return modificationReussie;

        }

        /// <summary>
        /// Renvoie le string equivalent à la catégorie de la note
        /// </summary>
        /// <param name="etu">note dont on veut la catégorie</param>
        /// <returns>string equivalent à la catégorie de la note</returns>
        /// <author>Laszlo</author>
        private string getCatNote(Note note)
        {

            string noteCat="";
            switch (note.Categorie)
            {
                case CATEGORIE.ABSENTEISME:
                    noteCat = "Absenteisme";
                    break;
                case CATEGORIE.MEDICAL:
                    noteCat = "Medical";
                    break;
                case CATEGORIE.PERSONNEL:
                    noteCat = "Personnel";
                    break;
                case CATEGORIE.RESULTATS:
                    noteCat = "Resultats";
                    break;
                case CATEGORIE.AUTRE:
                    noteCat = "Autre";
                    break;
            }
            return noteCat;
        }

        /// <summary>
        /// Renvoie le string equivalent à la nature de la note
        /// </summary>
        /// <param name="etu">note dont on veut la nature</param>
        /// <returns>string equivalent à la nature de la note</returns>
        /// <author>Laszlo</author>
        private string getNatNote(Note note)
        {

            string noteNat = "";
            switch (note.Nature)
            {
                case NATURE.APPEL:
                    noteNat = "Appel";
                    break;
                case NATURE.MAIL:
                    noteNat = "Mail";
                    break;
                case NATURE.RDV:
                    noteNat = "Rdv";
                    break;
                case NATURE.LETTRE:
                    noteNat = "Lettre";
                    break;
                case NATURE.AUTRE:
                    noteNat = "Autre";
                    break;
            }
            return noteNat;
        }

    }
}
