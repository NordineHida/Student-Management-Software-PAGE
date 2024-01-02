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
        /// Ajoute un token à la BDD puis le récupère
        /// </summary>
        /// <param name="note">token à ajouter</param>
        /// <returns>Le token créé ou null</returns>
        /// <author>Laszlo</author>
        public Token? CreateToken(Utilisateur user)
        {
            // Création d'une connexion Oracle
            Token tokenCree = null;
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

                string requeteTokenExiste = String.Format("SELECT COUNT(*) FROM Token Where idUtilisateur={0}", idUser);
                OracleCommand TokenExiste = new OracleCommand(requeteTokenExiste, con.OracleConnexion);
                reader = TokenExiste.ExecuteReader();
                bool isInsertOk = false;
                //Vérifier si un token existe déja : si oui, le supprimer et créer un nuveau
                while (reader.Read())
                {
                    if (reader.GetInt32(reader.GetOrdinal("COUNT(*)")) == 0)
                    {
                        isInsertOk = true;
                    }
                    else
                    {
                        string requeteSuppression = String.Format("DELETE FROM TOKEN WHERE idUtilisateur={0}", idUser, DateTime.Now);
                        OracleCommand delete = new OracleCommand(requeteSuppression, con.OracleConnexion);
                        if (delete.ExecuteNonQuery() == 1)
                        {
                            isInsertOk=true;
                        }
                }
                }

                if (isInsertOk)
                {
                    DateTime dateTime = DateTime.Now.AddMinutes(10);
                    // On insère la ligne dans la BDD
                    // On crée la requête SQL
                    string requeteInsertion = String.Format("INSERT INTO Token(idUtilisateur,token,datelimite)" +
                            "VALUES({0}, '{1}', TO_DATE('{2}', 'DD-MM-YYYY HH24-MI-SS'))", idUser, user.Login, dateTime);


                    // On execute la requete
                    OracleCommand cmd = new OracleCommand(requeteInsertion, con.OracleConnexion);
                    //On verifie que la ligne est bien insérée, si oui on récupère ce Token
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        tokenCree = new Token(user, dateTime);
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
            return tokenCree;
        
        }

    }
}
