using PAGE.Model.StockageNoteConfidentielles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLite
{
    /// <summary>
    /// Classe de tests unitaires pour la gestion du stockage des notes confidentielles.
    /// </summary>
    public class UnitTestStockageNoteConfidentielle
    {
        private DatabaseManager _dbManager;
        private string _testDbConnectionString = "Data Source=testdatabase.db";

        /// <summary>
        /// Constructeur de la classe de tests. Initialise la base de données de test.
        /// </summary>
        public UnitTestStockageNoteConfidentielle()
        {
            _dbManager = new DatabaseManager(_testDbConnectionString);
            _dbManager.OpenConnection();
            InitializeTestDatabase(); // Initialise la base de données de test.
        }

        /// <summary>
        /// Initialise la bdd
        /// </summary>
        private void InitializeTestDatabase()
        {
            // Créez la table NoteConfidentielle pour votre base de données de test (si elle n'existe pas déjà).
            string createTableQuery = "CREATE TABLE IF NOT EXISTS NoteConfidentielle (NoteId INTEGER PRIMARY KEY AUTOINCREMENT, Titre TEXT, Description TEXT)";
            _dbManager.ExecuteNonQuery(createTableQuery);

            // Insérez des données de test dans la table.
            string insertTestDataQuery1 = "INSERT INTO NoteConfidentielle (Titre, Description) VALUES ('Note 1', 'Description de la note 1')";
            string insertTestDataQuery2 = "INSERT INTO NoteConfidentielle (Titre, Description) VALUES ('Note 2', 'Description de la note 2')";

            _dbManager.ExecuteNonQuery(insertTestDataQuery1);
            _dbManager.ExecuteNonQuery(insertTestDataQuery2);
        }

        /// <summary>
        /// Nettoie les ressources utilisées par les tests.
        /// </summary>
        public void Dispose()
        {
            // Supprimez les données de test de la table NoteConfidentielle.
            string deleteTestDataQuery = "DELETE FROM NoteConfidentielle";
            _dbManager.ExecuteNonQuery(deleteTestDataQuery);

            _dbManager.CloseConnection();
        }


        /// <summary>
        /// Teste l'insertion d'une nouvelle note confidentielle dans la base de données.
        /// </summary>
        [Fact]
        public void TestInsertNote()
        {
            // Arrange
            string titre = "Nouvelle Note";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            // Act
            _dbManager.InsertNote(titre, description);

            // Assert
            DataTable result = _dbManager.GetAllNotes();
            Assert.NotEmpty(result.Rows);
        }

        /// <summary>
        /// Teste la mise à jour d'une note confidentielle existante dans la base de données.
        /// </summary>
        [Fact]
        public void TestUpdateNote()
        {
            // Arrange
            int noteId = 1; 
            string nouveauTitre = "Nouveau Titre";
            string nouvelleDescription = "Nouvelle Description";

            // Act
            _dbManager.UpdateNote(noteId, nouveauTitre, nouvelleDescription);

            // Assert
            DataRow note = _dbManager.GetNoteById(noteId);
            Assert.NotNull(note);
            Assert.Equal(nouveauTitre, note["Titre"].ToString());
            Assert.Equal(nouvelleDescription, note["Description"].ToString());
        }

        /// <summary>
        /// Teste la suppression d'une note confidentielle par son ID.
        /// </summary>
        [Fact]
        public void TestDeleteNoteById()
        {
            // Arrange
            int noteId = 1; 

            // Act
            _dbManager.DeleteNoteById(noteId);

            // Assert
            DataRow note = _dbManager.GetNoteById(noteId);
            Assert.Null(note);
        }
    
    }

}
