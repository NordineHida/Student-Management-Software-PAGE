using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PAGE.Model.StockageSQLite
{
    /// <summary>
    /// Classe gérant la base de données des notes confidentielles
    /// </summary>
    public class SQLiteBDD
    {
        private string connectionString;

        public SQLiteBDD(string dbFileName)
        {
            connectionString = $"Data Source={dbFileName};Version=3;";
        }

        /// <summary>
        /// Créer la database
        /// </summary>
        public void CreateDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableSQL = "CREATE TABLE IF NOT EXISTS NoteConfidentiel (IdNote INT PRIMARY KEY AUTOINCREMENT, Titre VARCHAR(50), Description TEXT);";
                using (SQLiteCommand command = new SQLiteCommand(createTableSQL, connection))
                {
                     command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Insérer une note
        /// </summary>
        /// <param name="titre">titre de la note</param>
        /// <param name="description">description de la note</param>
        public void InsertNote(string titre, string description)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertSQL = "INSERT INTO NoteConfidentiel (Titre, Description) VALUES (@Titre, @Description);";
                using (SQLiteCommand command = new SQLiteCommand(insertSQL, connection))
                {
                    command.Parameters.AddWithValue("@Titre", titre);
                    command.Parameters.AddWithValue("@Description", description);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        
        /// <summary>
        /// Mettre à jour une note
        /// </summary>
        /// <param name="id">id de la note à changer</param>
        /// <param name="newTitre">son nouveau titre</param>
        /// <param name="newDescription">sa nouvelle description</param>
        public void UpdateNote(int id, string newTitre, string newDescription)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string updateSQL = "UPDATE NoteConfidentiel SET Titre = @NewTitre, Description = @NewDescription WHERE IdNote = @Id;";
                using (SQLiteCommand command = new SQLiteCommand(updateSQL, connection))
                {
                    command.Parameters.AddWithValue("@NewTitre", newTitre);
                    command.Parameters.AddWithValue("@NewDescription", newDescription);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Supprimer une note
        /// </summary>
        /// <param name="id">id de la note à supprimer</param>
        public void DeleteNote(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteSQL = "DELETE FROM NoteConfidentiel WHERE IdNote = @Id;";
                using (SQLiteCommand command = new SQLiteCommand(deleteSQL, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }



    }

}
