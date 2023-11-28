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
        /// <author>Yamato/Nordine</author>
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
                    string requete = $"INSERT INTO Annee (anneeDebut) VALUES ({annee})";

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

        /// <summary>
        /// Supprime une année et tout ce qu'il y a en rapport (promotion, role)
        /// /// </summary>
        /// <param name="annee">année à supprimer</param>
        /// <returns>true si la suppression est effectué</returns>
        /// <author>Nordine</author>
        public bool DeleteAnnee(int? annee)
        {
            bool suppressionReussie = false;
            if (annee!=null)
            {
                Connection con = new Connection();

                try
                {                    
                    // Commencer une transaction
                    OracleTransaction transaction = con.OracleConnexion.BeginTransaction();

                    try
                    {
                        // Supprimer les promotions/etudiant associées à l'année
                        string deletePromotionEtudiant = $"DELETE FROM promotion_etudiant WHERE idpromotion IN (SELECT idpromotion FROM promotion WHERE anneedebut = {annee})";
                        OracleCommand deletePromotionEtudiantcmd = new OracleCommand(deletePromotionEtudiant, con.OracleConnexion);
                        deletePromotionEtudiantcmd.ExecuteNonQuery();


                        // Supprimer les promotions associées à l'année
                        string deletePromotions = $"DELETE FROM Promotion WHERE anneeDebut = {annee}";
                        OracleCommand deletePromotionsCmd = new OracleCommand(deletePromotions, con.OracleConnexion);
                        deletePromotionsCmd.ExecuteNonQuery();

                        // Supprimer l'année
                        string deleteAnnee = $"DELETE FROM Annee WHERE anneeDebut = {annee}";
                        OracleCommand deleteAnneeCmd = new OracleCommand(deleteAnnee, con.OracleConnexion);
                        deleteAnneeCmd.ExecuteNonQuery();

                        // Supprimer les rôles pendant cette année
                        string deleteAnneeRole = $"delete from roleutilisateur where annee = {annee}";
                        OracleCommand deleteAnneeRoleCmd = new OracleCommand(deleteAnneeRole, con.OracleConnexion);
                        deleteAnneeRoleCmd.ExecuteNonQuery();

                        // Valider la transaction si toutes les opérations ont réussi
                        transaction.Commit();
                        suppressionReussie = true;
                    }
                    catch (Exception ex)
                    {
                        // En cas d'erreur, annuler la transaction
                        transaction.Rollback();
                        Console.WriteLine($"Erreur lors de la suppression en cascade : {ex.Message}");
                    }
                }
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

    }
}
