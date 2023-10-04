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
            // Création d'une connexion Oracle
            //OracleConnection conn = ConnexionOracle.Instance.GetConnection();
            //conn.Open();


            List<Etudiant> etudiants = new List<Etudiant>();

            string connexionString = "User Id = IQ_BD_HIDA; Password = HIDA0000; Data Source = srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr";       //TROUVER LE CONNEXION STRING ICI
            OracleConnection con = new OracleConnection(connexionString);

            try
            {
                //conn.Open();

                con.Open();


                // Création d'une commande Oracle
                OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant", con);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   // string res = reader.

                    //On récupere le caractère du sexe en BDD et on le converti avec l'énumération 
                    char sexeBDD = reader.GetString(reader.GetOrdinal("sexe"))[0];
                    //Par défaut le sexe est autre
                    SEXE sexeEtu = SEXE.AUTRE;
                    switch (sexeBDD)
                    {
                        case 'F':
                            sexeEtu = SEXE.FEMININ;
                            break;
                        case 'M':
                            sexeEtu = SEXE.MASCULIN;
                            break;
                    }

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
                    int telFixe = reader.GetInt32(reader.GetOrdinal("telFixe"));
                    int telPortable = reader.GetInt32(reader.GetOrdinal("telPortable"));
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
                        telFixe,
                        telPortable,
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
        /*
        /// <summary>
        /// Essaye d'ajouter un nouvel etudiant et renvoi si on a réussi
        /// </summary>
        /// <param name="etu">etudiant qu'on veut ajouter</param>
        /// <returns>si l'ajout est un succes</returns>
        public bool AddUser(Etudiant? etu)
        {
            bool res = false;
            if (etu != null)
            {
                // Création d'une connexion Oracle
                OracleConnection conn = ConnexionOracle.Instance.GetConnection();

                try
                {
                    conn.Open();



                    // Création d'une commande Oracle
                    OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant WHERE numApogee = :numApogee", conn);

                    OracleDataReader reader = cmd.ExecuteReader();

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
                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return res;
        }*/
    }
}
