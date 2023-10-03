using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PAGE.Model.StockageSQLite
{
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

                string createTableSQL = "CREATE TABLE IF NOT EXISTS NoteConfidentiel (Titre VARCHAR(50), Description TEXT);";
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
        /// <param name="desc">description de la note</param>
        public void InsertNote(string titre, string desc)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertSQL = "INSERT INTO NoteConfidentiel (Titre) VALUES (@Titre); INSERT INTO NoteConfidentiel (Description) VALUES (@Description);";
                using (SQLiteCommand command = new SQLiteCommand(insertSQL, connection))
                {
                    command.Parameters.AddWithValue("@Titre", titre);
                    command.Parameters.AddWithValue("@Description", desc);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Renvoie toute les notes
        /// </summary>
        /// <returns>notes des étudiants</returns>
        public DataTable GetNote()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectSQL = "SELECT * FROM NoteConfidentiel";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectSQL, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        /// <summary>
        /// Met à jour la note
        /// </summary>
        /// <param name="titre">titre de la note à changer</param>
        /// <param name="nouveauTitre">nouveau titre de la note</param>
        /// <param name="nouvelleDesc">nouvelle description de la note</param>
        public void UpdateNote(int titre, string nouveauTitre, string nouvelleDesc)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string updateSQL = "UPDATE NoteConfidentiel SET Titre = @NouveauTitre WHERE Titre = @Titre; UPDATE NoteConfidentiel SET Description = @NouvelleDesc WHERE Titre = @Titre;";
                using (SQLiteCommand command = new SQLiteCommand(updateSQL, connection))
                {
                    command.Parameters.AddWithValue("@NouveauTitre", nouveauTitre);
                    command.Parameters.AddWithValue("@NouvelleDesc", nouvelleDesc);
                    command.Parameters.AddWithValue("@Titre", titre);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Supprime une note en fonction du titre
        /// </summary>
        /// <param name="titre">titre de la note à supprimer</param>
        public void DeleteNote(string titre)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteSQL = "DELETE FROM NoteConfidentiel WHERE Titre = @Titre";
                using (SQLiteCommand command = new SQLiteCommand(deleteSQL, connection))
                {
                    command.Parameters.AddWithValue("@Titre", titre);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }


    }

}
