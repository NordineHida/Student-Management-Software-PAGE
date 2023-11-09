using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    public class EtudiantDAOOracle : IEtuDAO
    {

        #region Singleton
        private static EtudiantDAOOracle instance;

        /// <summary>
        /// Seul instance de DAO d'étudiant 
        /// </summary>
        /// <author>Nordine</author>
        public static EtudiantDAOOracle Instance
        {
            get
            {
                if (instance == null) instance = new EtudiantDAOOracle();
                return instance;
            }
        }

        private EtudiantDAOOracle()
        {

        }
        #endregion


        /// <summary>
        /// Renvoi tout les étudiants de la BDD Oracle
        /// </summary>
        /// <returns>la liste des étudiants de la BDD Oracle</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu()
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste d'étudiant à renvoyer
            List<Etudiant> etudiants = new List<Etudiant>();

            try
            {
                // Création d'une commande Oracle pour récuperer l'ensemble des éléments de tout les étudiants
                OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant", con.OracleConnexion);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //On récupere le caractère du sexe en BDD et on le converti avec l'énumération 
                    string sexeBDD = reader.GetString(reader.GetOrdinal("sexe"));
                    //Par défaut le sexe est autre
                    SEXE sexeEtu = SEXE.AUTRE;
                    switch (sexeBDD)
                    {
                        case "F":
                            sexeEtu = SEXE.FEMININ;
                            break;
                        case "M":
                            sexeEtu = SEXE.MASCULIN;
                            break;
                    }

                    //Récupération(lecture) de tous les éléments d'un étudiant en bdd
                    int numApogee = reader.GetInt32(reader.GetOrdinal("numApogee"));
                    string nom = reader.GetString(reader.GetOrdinal("nom"));
                    string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                    string typeBac = reader.GetString(reader.GetOrdinal("typeBac"));
                    string mail = reader.GetString(reader.GetOrdinal("mail"));
                    string groupe = reader.GetString(reader.GetOrdinal("groupe"));
                    bool estBoursier = reader.GetString(reader.GetOrdinal("estBoursier")) == "OUI"; // Convertir en booléen
                    string regimeFormation = reader.GetString(reader.GetOrdinal("regimeFormation"));

                    //champs complémentaire dont on vérifier s'il existe avant de les affecters
                    DateTime dateNaissance;
                    if (!reader.IsDBNull(reader.GetOrdinal("dateNaissance")))
                    {
                        dateNaissance = reader.GetDateTime(reader.GetOrdinal("dateNaissance"));
                    }
                    else
                    {
                        // Gestion du cas où dateNaissance est NULL dans la base de données
                        dateNaissance = DateTime.MinValue; // ou une autre valeur par défaut
                    }

                    string login;
                    if (!reader.IsDBNull(reader.GetOrdinal("login")))
                    {
                        login = reader.GetString(reader.GetOrdinal("login"));
                    }
                    else
                    {
                        // Gestion du cas où login est NULL dans la base de données
                        login = string.Empty; // ou une autre valeur par défaut
                    }

                    long telFixe;
                    if (!reader.IsDBNull(reader.GetOrdinal("telFixe")))
                    {
                        telFixe = reader.GetInt64(reader.GetOrdinal("telFixe"));
                    }
                    else
                    {
                        // Gestion du cas où telFixe est NULL dans la base de données
                        telFixe = 0; // ou une autre valeur par défaut
                    }

                    long telPortable;
                    if (!reader.IsDBNull(reader.GetOrdinal("telPortable")))
                    {
                        telPortable = reader.GetInt32(reader.GetOrdinal("telPortable"));
                    }
                    else
                    {
                        // Gestion du cas où telPortable est NULL dans la base de données
                        telPortable = 0; // ou une autre valeur par défaut
                    }

                    string adresse;
                    if (!reader.IsDBNull(reader.GetOrdinal("adresse")))
                    {
                        adresse = reader.GetString(reader.GetOrdinal("adresse"));
                    }
                    else
                    {
                        // Gestion du cas où adresse est NULL dans la base de données
                        adresse = string.Empty; // ou une autre valeur par défaut
                    }

                    // Création de l'objet Etudiant en utilisant les variables
                    Etudiant etudiant = new Etudiant(
                        numApogee,
                        nom,
                        prenom,
                        sexeEtu,
                        typeBac,
                        mail,
                        groupe,
                        estBoursier,
                        regimeFormation,
                        dateNaissance,
                        login,
                        (int)telFixe,
                        (int)telPortable,
                        adresse
                    );


                    etudiants.Add(etudiant);
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
            return etudiants;

        }


        /// <summary>
        /// Ajoute un nouvelle étudiant (ou le modifie s'il existe déjà)
        /// </summary>
        /// <param name="etu">etudiant qu'on veut ajouter</param>
        /// <returns>si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etu)
        {
            bool ajoutReussi = false;

            if (etu != null)
            {
                //Si l'étudiant existe
                if (IsEtudiantExist(etu))
                {
                    //on le modifie
                    ajoutReussi = ModifierEtudiant(etu);
                }
                //sinon on le créer
                else
                {   
                    ajoutReussi = CreerEtudiant(etu);
                }
            }
            return ajoutReussi;
        }

        /// <summary>
        /// Renvoi le string equivalent au sexe de l'étudiant
        /// </summary>
        /// <param name="etu">etudiant dont on veut le sexe</param>
        /// <returns>string equivalent au sexe de l'étudiant</returns>
        /// <author>Nordine</author>
        private string getSexeString(Etudiant etu)
        {
  
            string etuSexe;
            switch (etu.Sexe)
            {
                case SEXE.FEMININ:
                    etuSexe = "F";
                    break;
                case SEXE.MASCULIN:
                    etuSexe = "M";
                    break;
                default:
                    etuSexe = "A";
                    break;
            }
            return etuSexe;
        }

        /// <summary>
        /// Modifie l'étudiant existant
        /// </summary>
        /// <param name="etu">etudiant à modifier</param>
        /// <returns>si la modification est un succès</returns>
        /// <author>Nordine</author>
        private bool ModifierEtudiant(Etudiant etu)
        {
            bool ajoutReussi = false;

            // Création d'une connexion Oracle
            Connection con = new Connection();


            string etuSexe = getSexeString(etu);

            string estBoursier = etu.EstBoursier ? "OUI" : "NON";

            try
            {
                // L'étudiant existe déjà, nous devons effectuer une mise à jour
                string updateQuery = string.Format(@"UPDATE Etudiant
                                                   SET nom = '{0}', prenom = '{1}', sexe = '{2}', typeBac = '{3}', mail = '{4}', groupe = '{5}', estBoursier = '{6}', regimeFormation = '{7}', dateNaissance = TO_DATE('{8}', 'YYYY-MM-DD'), adresse = '{9}', telPortable = {10}, telFixe = {11}, login = '{12}'
                                                   WHERE numApogee = {13}",
                                                  etu.Nom, etu.Prenom, etuSexe, etu.TypeBac, etu.Mail, etu.Groupe,
                                                  estBoursier, etu.TypeFormation, etu.DateNaissance.Date.ToString("yyyy-MM-dd"),
                                                  etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login, etu.NumApogee);

                OracleCommand updateCmd = new OracleCommand(updateQuery, con.OracleConnexion);

                if (updateCmd.ExecuteNonQuery() == 1)
                {
                    ajoutReussi = true;
                }
            }
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
            return ajoutReussi;
        }



        /// <summary>
        /// Creer un étudiant sur la bdd 
        /// </summary>
        /// <param name="etu">etudiant à creer</param>
        /// <returns>si la creation est un succès</returns>
        /// <author>Nordine</author>
        private bool CreerEtudiant(Etudiant etu)
        {

            bool ajoutReussi = false;

            // Création d'une connexion Oracle
            Connection con = new Connection();


            string etuSexe = getSexeString(etu);

            string estBoursier = etu.EstBoursier ? "OUI" : "NON";

            try
            {
                string insertQuery = string.Format(@"INSERT INTO Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login)
                                                   VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', TO_DATE('{9}', 'YYYY-MM-DD'), '{10}', {11}, {12}, '{13}')",
                                                  etu.NumApogee, etu.Nom, etu.Prenom, etuSexe, etu.TypeBac, etu.Mail, etu.Groupe,
                                                  estBoursier, etu.TypeFormation, etu.DateNaissance.Date.ToString("yyyy-MM-dd"),
                                                  etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login);

                OracleCommand updateCmd = new OracleCommand(insertQuery, con.OracleConnexion);

                if (updateCmd.ExecuteNonQuery() == 1)
                {
                    ajoutReussi = true;
                }
            }
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
            return ajoutReussi;
        }

        /// <summary>
        /// Renvoi si l'étudiant existe dans la bdd
        /// </summary>
        /// <param name="etu">etudiant à cherché</param>
        /// <returns>si létudiant existe déjà dans la bdd</returns>
        /// <author>Nordine</author>
        private bool IsEtudiantExist(Etudiant etu)
        {
            bool etudiantExist = false;
            // Création d'une connexion Oracle
            Connection con = new Connection();
            try
            {
                // On vérifie si un étudiant avec le même numéro d'apogée existe déjà
                string checkIfExistsQuery = $"SELECT COUNT(*) FROM Etudiant WHERE numApogee = {etu.NumApogee}";
                OracleCommand checkIfExistsCmd = new OracleCommand(checkIfExistsQuery, con.OracleConnexion);
                int existingCount = Convert.ToInt32(checkIfExistsCmd.ExecuteScalar());
                if (existingCount > 0)
                {
                    etudiantExist = true;
                }

            }                
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
            return etudiantExist;
        }



        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu)
        {
            bool ajoutReussi = true;

            //Pour tout les étudiants de la liste
            foreach (Etudiant etu in listeEtu)
            {
                //on essaye de les ajouter
                try
                {
                    //On ajoute et on récupere le bool de succès
                    bool succes = EtuManager.Instance.AddEtu(etu);

                    //Si le succes est faux lors d'un ajout, l'ajout total est concidéré comme un echec
                    if (!succes)
                    {
                        ajoutReussi = false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return ajoutReussi;
        }

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
    }
}
