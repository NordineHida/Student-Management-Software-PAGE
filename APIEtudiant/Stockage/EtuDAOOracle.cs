using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;
using PAGE.Model;
using PAGE.Stockage;

namespace APIEtudiant.Stockage
{
    public class EtudiantDAOOracle : IEtuDAO
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

            //Liste d'étudiant à renvoyer
            List<Etudiant> etudiants = new List<Etudiant>();

            try
            {
                // Vérifiez si la connexion n'est pas déjà ouverte.
                if (con.State != ConnectionState.Open)
                {
                    con.Open(); // Ouvrez la connexion si elle n'est pas déjà ouverte.
                }


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


        /// <summary>
        /// Essaye d'ajouter un nouvel etudiant et renvoi si on a réussi
        /// </summary>
        /// <param name="etu">etudiant qu'on veut ajouter</param>
        /// <returns>si l'ajout est un succes</returns>
        public bool AddEtu(Etudiant? etu)
        {
            bool ajoutReussi = false;
            if (etu != null)
            {
                // Création d'une connexion Oracle
                OracleConnection con = ConnexionOracle.Instance.GetConnection();

                try
                {
                    con.Open(); // Ouvrez la connexion si elle n'est pas déjà ouverte.


                    //On adapte l'énumeration du sexe de l'étudiant 
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

                    //On adapte le booleen estBoursier pour avoir "OUI" pour true et "NON" pour false
                    string estBoursier;
                    if (etu.EstBoursier) estBoursier = "OUI";
                    else estBoursier = "NON";

                    // On créer la requête SQL
                    /*string requete = String.Format("INSERT INTO Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login)" +
                        "VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', TO_DATE('{9}', 'YYYY-MM-DD'), '{10}', {11}, {12}, '{13}')",etu.NumApogee,etu.Nom,etu.Prenom,etuSexe,etu.TypeBac, etu.Mail, etu.Groupe
                        , estBoursier, etu.TypeFormation, etu.DateNaissance.Date.ToString("yyyy-MM-dd"), etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login);

                    */
                    string requete = "INSERT INTO Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login) VALUES(23456755, 'Bobet', 'Alice', 'F', 'Bac STI2D', 'bobetalice@gmail.com', 'D', 'NON', 'continue', TO_DATE('2001-09-11', 'YYYY-MM-DD'), '42 Route de Valons', 065844851, 098456512, 'ab656551')";

                    Console.WriteLine(requete);

                    //On execute la requete
                    OracleCommand cmd = new OracleCommand(requete, con);


                    //On verifie que la ligne est bien inséré, si oui on passe le bool à true
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        ajoutReussi = true;
                    }


                    Console.WriteLine(ajoutReussi);

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
