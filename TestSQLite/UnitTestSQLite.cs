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
            
        }

        [Fact]
        public void TestDeleteNote()
        {
           
        }
    }
}
