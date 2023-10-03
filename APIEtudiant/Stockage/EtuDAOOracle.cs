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
            OracleConnection conn = null;
            List<Etudiant> etudiants = new List<Etudiant>();

            try
            {
                // Création d'une connexion Oracle
                conn = new OracleConnection();
                conn.ConnectionString = "User Id=IQ_BD_HIDA;Password=HIDA0000;Data Source=srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr;";
                conn.Open();

                // Création d'une commande Oracle
                OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant WHERE numApogee = :numApogee", conn);

                // Paramètre :numApogee
                cmd.Parameters.Add(new OracleParameter(":numApogee", OracleDbType.Int32)).Value = 12345; // Remplacez 12345 par la valeur souhaitée !!!!!!!!!!!!

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
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

                    Etudiant etudiant = new Etudiant(
                        reader.GetInt32(reader.GetOrdinal("numApogee")),
                        reader.GetString(reader.GetOrdinal("nom")),
                        reader.GetString(reader.GetOrdinal("prenom")),
                        sexeEtu,
                        reader.GetString(reader.GetOrdinal("typeBac")),
                        reader.GetString(reader.GetOrdinal("mail")),
                        reader.GetString(reader.GetOrdinal("groupe")),
                        reader.GetString(reader.GetOrdinal("estBoursier")) == "OUI", // Convertir en booléen
                        reader.GetString(reader.GetOrdinal("regimeFormation")),
                        reader.GetDateTime(reader.GetOrdinal("dateNaissance")),
                        reader.GetString(reader.GetOrdinal("login")),
                        reader.GetInt32(reader.GetOrdinal("telFixe")),
                        reader.GetInt32(reader.GetOrdinal("telPortable")),
                        reader.GetString(reader.GetOrdinal("adresse"))
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
            return etudiants;

        }
    }
}
