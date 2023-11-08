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
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant? etu)
        {
            bool ajoutReussi = false;
            if (etu != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {


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
                    string requete = String.Format("INSERT INTO Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login)" +
                        "VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', TO_DATE('{9}', 'YYYY-MM-DD'), '{10}', {11}, {12}, '{13}')",etu.NumApogee,etu.Nom,etu.Prenom,etuSexe,etu.TypeBac, etu.Mail, etu.Groupe
                        , estBoursier, etu.TypeFormation, etu.DateNaissance.Date.ToString("yyyy-MM-dd"), etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login);

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

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo</author>
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
                    string requete = String.Format("DELETE FROM NOTE WHERE " +
                        "categorie='{0}' AND nature='{1}' AND commentaire='{2}' AND apogeeEtudiant='{3}'", note.Categorie, note.Nature, note.Commentaire, note.ApogeeEtudiant);


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
            //Liste d'étudiant à renvoyer
            List<Note> notes = new List<Note>();

            try
            {
                string requete = String.Format("SELECT categorie,datePublication,nature,commentaire,apogeeEtudiant FROM Note WHERE apogeeEtudiant='{0}", apogeeEtudiant);
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
    }
}
