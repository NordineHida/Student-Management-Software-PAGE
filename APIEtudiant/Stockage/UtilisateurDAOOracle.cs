using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;
using System.Text;
using APIEtudiant.Model.Enumerations;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Dao des utilisateurs pour la base de donnée oracle
    /// </summary>
    /// <author>Laszlo</author>
    public class UtilisateurDAOOracle : IUtilisateurDAO
    {
        /// <summary>
        /// Ajoute un utilisateur à la BDD
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <param name="annee">annee pour laquelle le role donne est actif</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        // <summary> 
        public bool CreateUtilisateur(Utilisateur? user, int annee)
        {
            bool ajoutReussi = false;
            if (user != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    // On crée la requête SQL
                    string requeteCreationUtilisateur = String.Format("INSERT INTO Utilisateur(idUtilisateur,login,hashPassword)" +
                        "VALUES(0, '{0}','{1}')",user.Login,user.HashMdp);

                    
                    //On execute la requete
                    OracleCommand cmdCreationUtilisateur = new OracleCommand(requeteCreationUtilisateur, con.OracleConnexion);


                    //On verifie que la ligne est bien inséré, si oui on passe le bool à true
                    if (cmdCreationUtilisateur.ExecuteNonQuery() == 1)
                    {
                        //On récupère l'ID
                        int idUser = 0;
                        string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'", user.Login);

                        OracleCommand getId = new OracleCommand(getIdRequete, con.OracleConnexion);
                        OracleDataReader reader = getId.ExecuteReader();

                        while (reader.Read())
                        {
                            idUser = reader.GetInt32(reader.GetOrdinal("idUtilisateur"));
                        }

                        string requeteInitialisationRole = String.Format("INSERT INTO RoleUtilisateur (annee,idUtilisateur,idRole) VALUES ({0},'{1}',5)", annee, idUser);
                        //On execute la requete
                        OracleCommand cmdInitialisationRole = new OracleCommand(requeteInitialisationRole, con.OracleConnexion);

                        {
                            if (cmdInitialisationRole.ExecuteNonQuery() == 1)
                            {
                                ajoutReussi = true;
                            }
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
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste d'étudiant à renvoyer
            List<Utilisateur> users = new List<Utilisateur>();

            try
            {
                // Création d'une commande Oracle pour récuperer l'ensemble des éléments de tout les étudiants
                OracleCommand cmd = new OracleCommand("SELECT login,hashPassword FROM Utilisateur", con.OracleConnexion);

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Récupération(lecture) de tous les éléments d'un étudiant en bdd
                    
                    string login = reader.GetString(reader.GetOrdinal("login"));
                    string hashPassword = reader.GetString(reader.GetOrdinal("hashPassword"));
                    // Création de l'objet Utilisateur en utilisant les variables
                    Utilisateur user = new Utilisateur(login, hashPassword);

                    //On récupère l'ID
                    int idUser = 0;
                    string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'", user.Login);

                    OracleCommand getId = new OracleCommand(getIdRequete, con.OracleConnexion);
                    OracleDataReader readerId = getId.ExecuteReader();

                    while (readerId.Read())
                    {
                        idUser = readerId.GetInt32(readerId.GetOrdinal("idUtilisateur"));
                    }


                    OracleCommand cmdRoleCount = new OracleCommand(String.Format("Select COUNT(*) From RoleUtilisateur WHERE idUtilisateur='{0}'", idUser), con.OracleConnexion);
                    OracleDataReader readerRoleCount = cmdRoleCount.ExecuteReader();
                    while (readerRoleCount.Read())
                    {
                        if (readerRoleCount.GetInt32(readerRoleCount.GetOrdinal("Count(*)")) != 0)
                        {
                            OracleCommand cmdRole = new OracleCommand(String.Format("Select idRole, annee From RoleUtilisateur WHERE idUtilisateur='{0}'", idUser), con.OracleConnexion);
                            OracleDataReader readerRole = cmdRole.ExecuteReader();

                            while (readerRole.Read())
                            {
                                int annee = readerRole.GetInt32(readerRole.GetOrdinal("annee"));
                                int idRole = readerRole.GetInt32(readerRole.GetOrdinal("idRole"));
                                user.Roles[annee] = GetRoleById(idRole);
                            }
                        }
                    }
                    users.Add(user);
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
            return users;

        }

        /// <summary>
        /// Regarde si l'utilisateur avec le logon et le mdp envoyé en paramètres existe, si oui, le renvoie
        /// </summary>
        /// <param name="login">login de l'utilisateur dont on vérifie l'existence</param>
        /// <param name="mdp">mdp (hashé) de l'utilisateur dont on vérifie l'existence</param>
        /// <returns>l'utilisateur, s'il existe</returns>
        public Utilisateur? GetUtilisateurByLoginMDP(string login, string mdp)
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //étudiant à renvoyer
            Utilisateur user = null;

            try
            {

                // Création d'une commande Oracle pour récuperer tous les utilisateurs
                OracleCommand cmd = new OracleCommand(String.Format("SELECT login,hashPassword FROM Utilisateur WHERE login='{0}' AND hashPassword='{1}'", login, mdp), con.OracleConnexion);

                OracleDataReader readerUtilisateur = cmd.ExecuteReader();

                while (readerUtilisateur.Read())
                {
                    user = new Utilisateur(readerUtilisateur.GetString(readerUtilisateur.GetOrdinal("login")), readerUtilisateur.GetString(readerUtilisateur.GetOrdinal("hashPassword")));
                }

                //On récupère l'ID
                int idUser = -1;
                if (user != null)
                {
                    string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'", user.Login);

                    OracleCommand getId = new OracleCommand(getIdRequete, con.OracleConnexion);
                    OracleDataReader readerId = getId.ExecuteReader();

                    while (readerId.Read())
                    {
                        idUser = readerId.GetInt32(readerId.GetOrdinal("idUtilisateur"));
                    }
                }

                OracleCommand cmdRoleCount = new OracleCommand(String.Format("Select COUNT(*) From RoleUtilisateur WHERE idUtilisateur='{0}'", idUser), con.OracleConnexion);
                OracleDataReader readerRoleCount = cmdRoleCount.ExecuteReader();
                while (readerRoleCount.Read())
                {
                    if (readerRoleCount.GetInt32(readerRoleCount.GetOrdinal("Count(*)")) != 0)
                    {
                        OracleCommand cmdRole = new OracleCommand(String.Format("Select idRole, annee From RoleUtilisateur WHERE idUtilisateur='{0}'", idUser), con.OracleConnexion);
                        OracleDataReader readerRole = cmdRole.ExecuteReader();

                        while (readerRole.Read())
                        {
                            int annee = readerRole.GetInt32(readerRole.GetOrdinal("annee"));
                            int idRole = readerRole.GetInt32(readerRole.GetOrdinal("idRole"));
                            user.Roles[annee] = GetRoleById(idRole);
                        }
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
            return user;
        }


        /// <summary>
        /// Modifie le rôle d'un utilisateur
        /// </summary>
        /// <param name="user">utilisateur dont le rôle va être changé</param>
        /// <param name="role">nouveau role attribué</param>
        /// <returns>vrai si le changement a été effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public bool UpdateRole(Utilisateur user, ROLE role, int annee)
        {
            bool modifReussie = false;
            if (user != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    //On récupère l'ID
                    int idUser = 0;
                    string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'", user.Login);

                    OracleCommand getId = new OracleCommand(getIdRequete, con.OracleConnexion);
                    OracleDataReader reader = getId.ExecuteReader();

                    while (reader.Read())
                    {
                        idUser = reader.GetInt32(reader.GetOrdinal("idUtilisateur"));
                    }

                    int countRoleAnnee = 0;
                    OracleCommand getRoleAnnee = new OracleCommand(String.Format("SELECT COUNT(*) FROM RoleUtilisateur Where annee={0} AND idUtilisateur={1}", annee, idUser), con.OracleConnexion);
                    OracleDataReader readerRoleAnnee = getRoleAnnee.ExecuteReader();
                    while (readerRoleAnnee.Read())
                    {
                        countRoleAnnee = readerRoleAnnee.GetInt32(readerRoleAnnee.GetOrdinal("Count(*)"));
                    }
                    if (countRoleAnnee == 0)
                    {
                        string requeteInitialisationRole = String.Format("INSERT INTO RoleUtilisateur (annee,idUtilisateur,idRole) VALUES ({0},{1},{2}) ", annee, idUser,GetIdRole(role));
                        //On execute la requete
                        OracleCommand cmdInitialisationRole = new OracleCommand(requeteInitialisationRole, con.OracleConnexion);
                        {
                            if (cmdInitialisationRole.ExecuteNonQuery() == 1)
                            {
                                modifReussie = true;
                            }
                        }
                    }
                    else
                    {
                        string requeteModifRole = String.Format("UPDATE RoleUtilisateur SET idRole={0} WHERE idUtilisateur={1} AND annee={2}", GetIdRole(role),idUser, annee);
                        //On execute la requete
                        OracleCommand cmdModifRole = new OracleCommand(requeteModifRole, con.OracleConnexion);
                        {
                            if (cmdModifRole.ExecuteNonQuery() == 1)
                            {
                                modifReussie = true;
                            }
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
            return modifReussie;
        }

       
        private int GetIdRole(ROLE role)
        {
            int idRole;
            switch (role)
            {
                case ROLE.DIRECTEURDEPARTEMENT:
                    idRole = 0;
                    break;
                case ROLE.DIRECTEURETUDES1:
                    idRole = 1;
                    break;
                case ROLE.DIRECTEURETUDES2:
                    idRole = 2;
                    break;
                case ROLE.DIRECTEURETUDES3:
                    idRole = 3;
                    break;
                case ROLE.ADMIN:
                    idRole = 4;
                    break;
                default:
                    idRole = 5;
                    break;
            }
            return idRole;
        }

        private ROLE GetRoleById(int idRole)
        {
            ROLE role;
            switch (idRole)
            {
                case 0:
                    role = ROLE.DIRECTEURDEPARTEMENT;
                    break;
                case 1:
                    role = ROLE.DIRECTEURETUDES1;
                    break;
                case 2:
                    role = ROLE.DIRECTEURETUDES2;
                    break;
                case 3:
                    role = ROLE.DIRECTEURETUDES3;
                    break;
                case 4:
                    role = ROLE.ADMIN;
                    break;
                default:
                    role = ROLE.LAMBDA;
                    break;
            }
            return role;
        }

        }
    }
