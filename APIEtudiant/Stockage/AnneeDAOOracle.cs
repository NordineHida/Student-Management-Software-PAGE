using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    public class AnneeDAOOracle : IAnneeDAO
    {
        /// <summary>
        /// Ajoute une année à la BDD
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <returns>true si l'ajout est effectué</returns>
        /// <author>Yamato</author>
        public bool CreateAnnee(int? annee)
        {
            bool ajoutReussi = false;
            if (annee != null)
            {
                // Création d'une connexion Oracle
                Connection con = new Connection();

                try
                {
                    // On crée la requête SQL
                    string requete = String.Format("INSERT INTO ANNEE(anneeDebut)" +
                        "VALUES('{0}')", annee);


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
        /// Renvoie toutes les années de la bdd 
        /// </summary>
        /// <returns>liste des années</returns>
        /// <author>Yamato</author>
        public List<Annee> GetAllAnnee()
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste d'années à renvoyer
            List<Annee> annees = new List<Annee>();
            int anneeDebut=-1;

            try
            {
                // Création d'une commande Oracle pour récuperer toutes les années
                OracleCommand cmd = new OracleCommand("SELECT * FROM ANNEE", con.OracleConnexion);

                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // On récupère les annee de début
                    anneeDebut = reader.GetInt32(reader.GetOrdinal("anneeDebut"));

                    annees.Add(new Annee(anneeDebut));
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
            return annees;

        }
    }
}
