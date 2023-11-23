using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Dao des tokens pour la base de donnée oracle
    /// </summary>
    /// <author>Laszlo</author>
    public class TokenDAOOracle
    {
        /// <summary>
        /// Ajoute un token à la BDD
        /// </summary>
        /// <param name="note">token à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateToken(Utilisateur user)
        {
            bool ajoutReussi = false;
            if (user != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'",user.Login);

                    // On crée la requête SQL
                    string requete = String.Format("INSERT INTO Token(idUtilisateur,token,datelimite)" +
                        "VALUES({0}, '{1}', TO_DATE('{2}', 'YYYY-MM-DD'))",getIdRequete,user.Login , DateTime.Now);


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
