using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Permet de creer et distribuer des connexion 
    /// </summary>
    /// <author>Nordine</author>
    public class Connection
    {

        /// <summary>
        /// String de connexion a la Base de donnée des étudiants
        /// </summary>
        /// <author>Nordine</author>
        private string connexionString = "User Id = IQ_BD_HIDA; Password = HIDA0000; Data Source = srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr";

        private OracleConnection connexion;

        /// <summary>
        /// Renvoi la connexion
        /// </summary>
        /// <author>Nordine</author>
        public OracleConnection OracleConnexion => connexion;

        /// <summary>
        /// Constructeur de connexion (ouvre la connexion)
        /// </summary>
        /// <author>Nordine</author>
        public Connection()
        {
            this.connexion = new OracleConnection(connexionString);
            this.connexion.Open();
        }

        /// <summary>
        /// Permet de fermer la connexion
        /// </summary>
        /// <author>Nordine</author>
        public void Close()
        {
            this.connexion.Close();
        }
    }
}
