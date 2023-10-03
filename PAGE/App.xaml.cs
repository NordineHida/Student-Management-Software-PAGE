using PAGE.Model.StockageSQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SQLiteBDD sqliteBdd;

        public App()
        {
            InitializeComponent();

            // Initialisation de la base de données
            sqliteBdd = new SQLiteBDD("sqlitedb.sqlite");
            sqliteBdd.CreateDatabase();
        }
    }
}
