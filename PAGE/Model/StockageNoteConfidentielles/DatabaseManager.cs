using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PAGE.Model.StockageNoteConfidentielles
{
    public class DatabaseManager
    {
        private SQLiteConnection _connection;

        public DatabaseManager(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public void ExecuteNonQuery(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            command.ExecuteNonQuery();
        }
    }
}
