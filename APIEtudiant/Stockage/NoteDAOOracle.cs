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
                        "VALUES(0, '{0}', TO_DATE('{1}', 'YYYY-MM-DD'), '{2}', '{3}', '{4}')", note.Categorie, note.DatePublication.Date.ToString("yyyy-MM-dd"), note.Nature, note.Commentaire, note.ApogeeEtudiant);


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
                    string categorie = reader.GetString(reader.GetOrdinal("categorie"));
                    DateTime datePublication = reader.GetDateTime(reader.GetOrdinal("datePublication"));
                    string nature = reader.GetString(reader.GetOrdinal("nature"));
                    string commentaire = reader.GetString(reader.GetOrdinal("commentaire"));

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
        /// Renvoie toutes les notes
        /// </summary>
        /// <returns>la liste de notes/returns>
        /// <author>Laszlo</author>
        public IEnumerable<Note> GetAllNotes()
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste d'étudiant à renvoyer
            List<Note> notes = new List<Note>();

            try
            {
                string requete = "SELECT categorie,datePublication,nature,commentaire,apogeeEtudiant FROM Note";
                // Création d'une commande Oracle pour récuperer l'ensemble des éléments de tout les étudiants
                OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    //Récupération(lecture) de tous les éléments d'un étudiant en bdd
                    string categorie = reader.GetString(reader.GetOrdinal("categorie"));
                    DateTime datePublication = reader.GetDateTime(reader.GetOrdinal("datePublication"));
                    string nature = reader.GetString(reader.GetOrdinal("nature"));
                    string commentaire = reader.GetString(reader.GetOrdinal("commentaire"));
                    int apogeeEtudiant = reader.GetInt32(reader.GetOrdinal("apogeeEtudiant"));

                    // Création de l'objet Etudiant en utilisant les variables
                    Note note = new Note(categorie, datePublication, nature, commentaire, apogeeEtudiant);

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
                                                   note.Categorie, note.DatePublication.Date.ToString("yyyy-MM-dd"), note.Nature, note.Commentaire, note.ApogeeEtudiant, note.IdNote);
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
    }
}
