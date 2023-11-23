using PAGE.Model;
using PAGE.Model.PatternObserveur;
using PAGE.Model.StockageNoteConfidentielles;
using System;
using System.Windows;

namespace PAGE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IObservateur
    {
        /// <summary>
        /// Constructeur d'appli qui initialise la base de donnée
        /// </summary>
        /// <author>Yamato & Nordine</author>
        public App()
        {
            this.InitializeComponent();

            //L'app regarde les paramètres (nordine)
            Parametre.Instance.Register(this);


            #region Yamato BDD SQLite
            // Création de la database
            string connectionString = "Data Source=noteConfidentielle.db";
            DatabaseManager dbManager = new DatabaseManager(connectionString);

            dbManager.OpenConnection(); // Ouverture de la connexion

            // Création de la table NoteConfidentielle
            string createTableQuery = "CREATE TABLE IF NOT EXISTS NoteConfidentielle (NoteId INTEGER PRIMARY KEY AUTOINCREMENT, Titre TEXT, Description TEXT)";
            dbManager.ExecuteNonQuery(createTableQuery);

            dbManager.CloseConnection(); // Fermeture de la connexion
            #endregion
        }

        /// <summary>
        /// Quand les parametres change on notifie l'app (pour changer la langue)
        /// </summary>
        /// <param name="Message">langue modifié dans les parametres</param>
        /// <author>Nordine</author>
        public void Notifier(string Message)
        {
            ResourceDictionary dictionnaire = new ResourceDictionary();

            switch (Message)
            {
                case "ANGLAIS":
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "FRANCAIS":
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
                default:
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
            }

            //Change la ressource utilisé par l'application
            App.Current.Resources.MergedDictionaries.Add(dictionnaire);
        }
    }
}
