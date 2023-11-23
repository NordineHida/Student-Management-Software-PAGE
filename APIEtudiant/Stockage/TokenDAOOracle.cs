using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Dao des tokens pour la base de donnée oracle
    /// </summary>
    /// <author>Laszlo</author>
    public class TokenDAOOracle : ITokenDAO
    {
        /// <summary>
        /// Ajoute un token à la BDD
        /// </summary>
        /// <param name="note">token à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public Token CreateToken(Utilisateur user)
        {
                Token tokenCree = new Token(-1, DateTime.Now);
                // Création d'une connexion Oracle
                Connection con = new Connection();
                
                try
                {
                    //On récupère l'ID
                    int idUser = 0;
                    string getIdRequete = String.Format("SELECT idUtilisateur FROM Utilisateur Where login='{0}'",user.Login);

                    OracleCommand getId = new OracleCommand(getIdRequete,con.OracleConnexion);
                    OracleDataReader reader = getId.ExecuteReader();

                    while (reader.Read())
                    {
                         idUser = reader.GetInt32(reader.GetOrdinal("idUtilisateur"));
                    }

                    string requeteTokenExiste = String.Format("SELECT * FROM Token Where idUtilisateur={0} AND datelimite>TO_DATE('{1}', 'YYYY-MM-DD'))", idUser, DateTime.Now);
                    OracleCommand TokenExiste = new OracleCommand(requeteTokenExiste, con.OracleConnexion);
                    reader = TokenExiste.ExecuteReader();

                //Vérifier si un token existe déja : si oui, le supprimer et créer un nuveau
                    while (reader.Read())
                    {
                        
                    }
                    // On crée la requête SQL
                    string requeteInsertion = String.Format("INSERT INTO Token(idUtilisateur,token,datelimite)" +
                        "VALUES({0}, '{1}', TO_DATE('{2}', 'YYYY-MM-DD'))",idUser,user.Login , DateTime.Now);


                    //On execute la requete
                    OracleCommand cmd = new OracleCommand(requeteInsertion, con.OracleConnexion);


                    //On verifie que la ligne est bien inséré, si oui on récupère ce Token
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        DateTime dateTime = DateTime.Now;
                        tokenCree = new Token(idUser, dateTime.AddMinutes(10));
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
            return tokenCree;
        }
    }
}
