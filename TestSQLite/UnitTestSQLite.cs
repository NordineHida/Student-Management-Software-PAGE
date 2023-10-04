using PAGE.Model.StockageSQLite;
using System.Data;

namespace TestSQLite
{
    /// <summary>
    /// Tests de chaque méthode de la classe SQLiteBDD
    /// </summary>
    public class UnitTestSQLite
    {
        private string dbFileName = "test.db";
        private SQLiteBDD db;

        public UnitTestSQLite()
        {
            db = new SQLiteBDD(dbFileName);
            db.CreateDatabase();
        }

        /// <summary>
        /// Test de création de la database
        /// </summary>
        [Fact]
        public void TestCreateDatabase()
        {
            Assert.True(File.Exists(dbFileName));
        }

        [Fact]
        public void TestInsertNote()
        {
            
        }

        [Fact]
        public void TestUpdateNote()
        {
            // Arrange
            db.InsertNote("Note 1", "Description 1");

            // Act
            db.UpdateNote("Note 1", "New Titre", "New Description");

            // Assert
            DataTable dataTable = db.GetAllNote();
            Assert.Equal(1, dataTable.Rows.Count);
            Assert.Equal("New Titre", dataTable.Rows[0]["Titre"]);
            Assert.Equal("New Description", dataTable.Rows[0]["Description"]);
        }

        [Fact]
        public void TestDeleteNote()
        {
            // Arrange
            db.InsertNote("Note 1", "Description 1");

            // Act
            db.DeleteNote('1');

            // Assert
            DataTable dataTable = db.GetAllNote();
            Assert.Empty(dataTable.Rows);
        }
    }
}
