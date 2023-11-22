using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    public class PieceJointeDAOOracle : IPieceJointeDAO
    {
        public bool SaveFilepath(PieceJointe pieceJointe)
        {
            bool ajoutReussi = false;
            // Création d'une connexion Oracle
            Connection con = new Connection();

            try
            {
                // On crée la requête SQL
                string requete = String.Format("INSERT INTO PieceJointe(idPieceJointe,filePath)" +
                    "VALUES(0, '{0}')", pieceJointe.FilePath);


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

            return ajoutReussi;
        }
    }
}
