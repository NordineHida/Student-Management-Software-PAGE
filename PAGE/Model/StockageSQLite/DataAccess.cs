using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace PAGE.Model.StockageSQLite
{
    /// <summary>
    /// Classe gérant l'accès aux données
    /// </summary>
    public static class DataAccess
    {
        /// <summary>
        /// Initialisation de la base de données
        /// </summary>
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("notesConfidentielles.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "notesConfidentielles.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS NoteConfidentielle (NoteId INTEGER PRIMARY KEY AUTOINCREMENT, Titre VARCHAR(255), Description VARCHAR(255))";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        /// <summary>
        /// Ajoute une note
        /// </summary>
        /// <param name="titre">titre de la note</param>
        /// <param name="description">description de la note</param>
        public static void AddNote(string titre, string description)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "notesConfidentielles.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO NoteConfidentielle VALUES (@Titre, @Description);";
                insertCommand.Parameters.AddWithValue("@Titre", titre);
                insertCommand.Parameters.AddWithValue("@Description", description);

                insertCommand.ExecuteReader();
            }

        }

        /// <summary>
        /// Permet d'obtenir une note avec son is
        /// </summary>
        /// <param name="id">id de la note</param>
        /// <returns>note voulue</returns>
        public static List<String> GetNote(int id)
        {
            List<String> note = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "notesConfidentielles.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand ("SELECT (Titre, Description) from NoteConfidentielle WHERE NoteId = @id", db);
                selectCommand.Parameters.AddWithValue("@id", id);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    note.Add(query.GetString(0));
                }
            }

            return note;
        }

        /// <summary>
        /// Supprime une note avec son id
        /// </summary>
        /// <param name="id">id de la note </param>
        public static void DeleteNote(int id)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "notesConfidentielles.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "DELETE FROM NoteConfidentielle WHERE NoteId = @id;";
                insertCommand.Parameters.AddWithValue("@id", id);

                insertCommand.ExecuteReader();
            }

        }
    }
}
