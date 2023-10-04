using System;
using System.Collections.Generic;
using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;
using PAGE.Model;
using PAGE.Stockage;

namespace APIEtudiant.Stockage
{
    public class EtudiantDAOOracle :IEtuDAO
    {

        #region Singleton
        private static EtudiantDAOOracle instance;

        /// <summary>
        /// Seul instance de DAO d'étudiant 
        /// </summary>
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
        public IEnumerable<Etudiant> GetAllEtu()
        {
            //Création d'une connexion Oracle
            OracleConnection con = ConnexionOracle.Instance.GetConnection();
            con.Open();

            List<Etudiant> etudiants = new List<Etudiant>();

            try
            {
                //conn.Open();

                con.Open();


                // Création d'une commande Oracle pour récuperer l'ensemble des éléments de tout les étudiants
                OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant", con);

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
                    DateTime dateNaissance = reader.GetDateTime(reader.GetOrdinal("dateNaissance"));
                    string login = reader.GetString(reader.GetOrdinal("login"));
                    long telFixe = reader.GetInt64(reader.GetOrdinal("telFixe"));
                    long telPortable = reader.GetInt32(reader.GetOrdinal("telPortable"));
                    string adresse = reader.GetString(reader.GetOrdinal("adresse"));

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
        

    }
}
