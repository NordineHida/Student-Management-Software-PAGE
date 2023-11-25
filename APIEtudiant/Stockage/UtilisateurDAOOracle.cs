using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;
using System.Text;

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
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        // <summary> 
        public bool CreateUtilisateur(Utilisateur? user)
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
                        "VALUES(0, '{0}','{1}')",user.Login,user.Mdp);

                    
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

                        string requeteInitialisationRole = String.Format("INSERT INTO RoleUtilisateur (annee,idUtilisateur,idRole) VALUES ({0},'{1}',6)", 2023, idUser);
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

        public string HashMdp(string mdp)
        {
            string mdpHashed = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(mdp));
                foreach (byte b in hashValue)
                {
                    mdpHashed += $"{b:X2}";
                }
            }
            return mdpHashed;
        }
    }
}
