using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;
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
        /// Dictionnaire pour la catégorieID / CATEGORIE 
        /// </summary>
        /// <author>Nordine</author>
        private static Dictionary<int, CATEGORIE> AssociationCategorieId = new Dictionary<int, CATEGORIE>
        {
            { 1, CATEGORIE.ABSENTEISME },
            { 2, CATEGORIE.PERSONNEL },
            { 3, CATEGORIE.MEDICAL },
            { 4, CATEGORIE.RESULTATS },
            { 5, CATEGORIE.ORIENTATION },
            { 6, CATEGORIE.AUTRE }
        };

        /// <summary>
        /// Dictionnaire pour la confidentialiteID / CONFIDENTIALITE 
        /// </summary>
        /// <author>Nordine</author>
        private static Dictionary<int, CONFIDENTIALITE> AssociationConfidentialiteId = new Dictionary<int, CONFIDENTIALITE>
        {
            { 1, CONFIDENTIALITE.MEDICAL },
            { 2, CONFIDENTIALITE.CONFIDENTIEL },
            { 3, CONFIDENTIALITE.INTERNE },
            { 4, CONFIDENTIALITE.PUBLIC }
        };

        /// <summary>
        /// Dictionnaire pour la natureID / NATURE 
        /// </summary>
        /// <author>Nordine</author>
        private static Dictionary<int, NATURE> AssociationNatureId = new Dictionary<int, NATURE>
        {
            { 1, NATURE.MAIL },
            { 2, NATURE.RDV },
            { 3, NATURE.LETTRE },
            { 4, NATURE.APPEL },
            { 5, NATURE.AUTRE }
        };
        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo/Nordine</author>
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
                    string requete = String.Format("INSERT INTO Note(idNote, idCategorie, datePublication, idNature, commentaire, apogeeEtudiant, idConfidentialite) " +
                        "VALUES(0, {0}, TO_DATE('{1}', 'YYYY-MM-DD'), {2}, '{3}', {4}, {5})",
                        AssociationCategorieId.FirstOrDefault(x => x.Value == note.Categorie).Key, note.DatePublication.Date.ToString("yyyy-MM-dd"),
                        AssociationNatureId.FirstOrDefault(x => x.Value == note.Nature).Key, note.Commentaire, note.ApogeeEtudiant,
                        AssociationConfidentialiteId.FirstOrDefault(x => x.Value == note.Confidentialite).Key);

                    // On execute la requete
                    using (OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion))
                    {
                        // On verifie que la ligne est bien inséré, si oui on passe le bool à true
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            ajoutReussi = true;
                        }
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


                    //On verifie que la ligne est bien supprimé, si oui on passe le bool à true
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
        /// Renvoi  toute les notes lié au numApogee
        /// </summary>
        /// <param name="apogeeEtudiant">apogee de l'étudiant dont on veut les notes</param>
        /// <returns>list de note de l'étudiant</returns>
        /// <author>Laszlo/Nordine</author>
        public IEnumerable<Note> GetAllNotesByApogee(int apogeeEtudiant)
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();

            //Liste de note à renvoyer
            List<Note> notes = new List<Note>();


            try
            {
                string requete = "SELECT idNote, idCategorie, datePublication, idNature, commentaire, idConfidentialite, apogeeEtudiant FROM Note WHERE apogeeEtudiant = :apogeeEtudiant";

                using (OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion))
                {
                    cmd.Parameters.Add("apogeeEtudiant", OracleDbType.Int32).Value = apogeeEtudiant;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idNote = reader.GetInt32(reader.GetOrdinal("idNote"));
                            int idCategorie = reader.GetInt32(reader.GetOrdinal("idCategorie"));
                            DateTime datePublication = reader.GetDateTime(reader.GetOrdinal("datePublication"));
                            int idNature = reader.GetInt32(reader.GetOrdinal("idNature"));
                            string commentaire = reader.IsDBNull(reader.GetOrdinal("commentaire")) ? string.Empty : reader.GetString(reader.GetOrdinal("commentaire"));
                            int idConfidentialite = reader.GetInt32(reader.GetOrdinal("idConfidentialite"));


                            // Convertir les valeurs des colonnes en utilisant les dictionnaires
                            CATEGORIE categorie = AssociationCategorieId[idCategorie];
                            NATURE nature = AssociationNatureId[idNature];
                            CONFIDENTIALITE confidentialite = AssociationConfidentialiteId[idConfidentialite];



                            // Création de l'objet Note en utilisant les valeurs récupérées de la base de données
                            Note note = new Note(categorie, datePublication,nature, commentaire, apogeeEtudiant, confidentialite);
                            note.IdNote = idNote;

                            notes.Add(note);
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return notes;
        }

        /// <summary>
        /// Modifie une note de la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine/Laszlo</author>
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
                                                   "SET idCategorie = {0}, " +
                                                   "datePublication = TO_DATE('{1}', 'YYYY-MM-DD'), " +
                                                   "idNature = {2}, " +
                                                   "commentaire = '{3}', " +
                                                   "idConfidentialite = {4} " +
                                                   "WHERE idNote = {5}",
                                                   AssociationCategorieId.FirstOrDefault(x => x.Value == note.Categorie).Key, note.DatePublication.Date.ToString("yyyy-MM-dd"), AssociationNatureId.FirstOrDefault(x => x.Value == note.Nature).Key, note.Commentaire, AssociationConfidentialiteId.FirstOrDefault(x => x.Value == note.Confidentialite).Key, note.IdNote);
                    // On exécute la requête
                    using (OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion))
                    {
                        // On vérifie que la ligne est bien modifiée, si oui on passe le bool à true
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            modificationReussie = true;
                        }
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
