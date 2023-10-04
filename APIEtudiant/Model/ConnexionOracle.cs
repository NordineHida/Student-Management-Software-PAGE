using APIEtudiant.Stockage;
using System;
using System.Collections.Generic;
using APIEtudiant.Model;
using Oracle.ManagedDataAccess.Client;
using PAGE.Model;
using PAGE.Stockage;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Permet de creer et distribuer des connexion a la base de données Oracle
    /// </summary>
    public class ConnexionOracle
    {
        #region Singleton
        private static ConnexionOracle instance;

        /// <summary>
        /// Seul instance de ConnexionOracle
        /// </summary>
        public static ConnexionOracle Instance
        {
            get
            {
                if (instance == null) instance = new ConnexionOracle();
                return instance;
            }
        }

        private ConnexionOracle()
        {

        }
        #endregion

        /// <summary>
        /// String de connexion a la Base de donnée des étudiants
        /// </summary>
        private string connexionString = "User Id = IQ_BD_HIDA; Password = HIDA0000; Data Source = srviq-ora:1521/orclpdb.iut21.u-bourgogne.fr";
        /*
        /// <summary>
        /// Renvoi une connexion oracle pour la BDD
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetConnection()
        {_
            OracleConnection conn = new OracleConnection(connexionString);
            return conn;
        }
        */
    }
}
