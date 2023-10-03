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
    }

}
