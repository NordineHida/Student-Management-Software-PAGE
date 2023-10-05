using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PAGE.Model.StockageNoteConfidentielles
{
    /// <summary>
    /// Gestion de la base de données
    /// </summary>
    public class DatabaseManager
    {
        private SQLiteConnection _connection;

        public DatabaseManager(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
        }

        /// <summary>
        /// Ouvre la connexion à la bdd
        /// </summary>
        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// Fermeture de la bdd
        /// </summary>
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Exécute une requête SELECT sans paramètres et retourne son résultat
        /// </summary>
        /// <param name="query">requête sql (select)</param>
        /// <returns>résultat de la requête</returns>
        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _connection);
            adapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Exécute une requête SELECT avec des paramètres et retourne son résultat
        /// </summary>
        /// <param name="query">requête sql (select)</param>
        /// <param name="parameters">parametre de la requête</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query, SQLiteParameter[] parameters)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        /// <summary>
        /// Exécute une requête INSERT, UPDATE, DELETE sans paramètre et retourne son résultat
        /// </summary>
        /// <param name="query">requète sql (insert, update, delete)</param>
        public void ExecuteNonQuery(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Exécute une requête INSERT, UPDATE, DELETE qui a des paramètre et retourne son résultat
        /// </summary>
        /// <param name="query">requète sql (insert, update, delete)</param>
        /// <param name="parameters">paramètres de la requète</param>
        public void ExecuteNonQuery(string query, SQLiteParameter[] parameters = null)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Récupère toutes les notes
        /// </summary>
        /// <returns>notes de toute la table</returns>
        public DataTable GetAllNotes()
        {
            string query = "SELECT * FROM NoteConfidentielle";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// Récupère une note à partir de son id
        /// </summary>
        /// <param name="noteId">id de la note</param>
        /// <returns>résultat de la requête</returns>
        public DataRow GetNoteById(int noteId)
        {
            string query = "SELECT * FROM NoteConfidentielle WHERE NoteId = @NoteId";
            SQLiteParameter parameter = new SQLiteParameter("@NoteId", DbType.Int32) { Value = noteId };

            DataTable result = ExecuteQuery(query, new SQLiteParameter[] { parameter });

            if (result.Rows.Count > 0)
            {
                return result.Rows[0];
            }
            else
            {
                return null; // La note n'a pas été trouvée.
            }
        }

        /// <summary>
        /// Insert une note 
        /// </summary>
        /// <param name="titre">titre de la note</param>
        /// <param name="description">description de la note</param>
        public void InsertNote(string titre, string description)
        {
            string query = "INSERT INTO NoteConfidentielle (Titre, Description) VALUES (@Titre, @Description)";
            SQLiteParameter[] parameters =
            {
            new SQLiteParameter("@Titre", DbType.String) { Value = titre },
            new SQLiteParameter("@Description", DbType.String) { Value = description }
        };
            ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Met à jour une note à partir de son Id
        /// </summary>
        /// <param name="noteId">id de la note à update</param>
        /// <param name="titre">nouveau titre de la note</param>
        /// <param name="description">nouvelle description de la note</param>
        public void UpdateNote(int noteId, string titre, string description)
        {
            string query = "UPDATE NoteConfidentielle SET Titre = @Titre, Description = @Description WHERE NoteId = @NoteId";
            SQLiteParameter[] parameters =
            {
            new SQLiteParameter("@NoteId", DbType.Int32) { Value = noteId },
            new SQLiteParameter("@Titre", DbType.String) { Value = titre },
            new SQLiteParameter("@Description", DbType.String) { Value = description }
        };
            ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Supprime une note à partir de son Id.
        /// </summary>
        /// <param name="noteId">ID de la note à supprimer.</param>
        public void DeleteNoteById(int noteId)
        {
            string query = "DELETE FROM NoteConfidentielle WHERE NoteId = @NoteId";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@NoteId", DbType.Int32) { Value = noteId }
            };
            ExecuteNonQuery(query, parameters);
        }

    }
}
