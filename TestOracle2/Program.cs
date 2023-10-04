using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOracle2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connexionString = "User Id = IQ_BD_HIDA; Password = HIDA0000; Data Source = srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr";       //TROUVER LE CONNEXION STRING ICI
            OracleConnection con = new OracleConnection(connexionString);

            con.Open();
            Console.WriteLine(con.DatabaseName);
        }
    }
}
