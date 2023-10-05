using PAGE.Model.StockageNoteConfidentielles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        public App()
        {
            this.InitializeComponent();

            // Création de la database
            string connectionString = "Data Source=noteConfidentielle.db";
            DatabaseManager dbManager = new DatabaseManager(connectionString);

            dbManager.OpenConnection(); // Ouverture de la connexion

            // Création de la table NoteConfidentielle
            string createTableQuery = "CREATE TABLE IF NOT EXISTS NoteConfidentielle (NoteId INTEGER PRIMARY KEY AUTOINCREMENT, Titre TEXT, Description TEXT)";
            dbManager.ExecuteNonQuery(createTableQuery);

            dbManager.CloseConnection(); // Fermeture de la connexion
        }
    }
}
