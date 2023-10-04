using PAGE.Model.StockageSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLite
{
    public class UnitTestDataAccess
    {
        [Fact]
        public void AddNote_NoteAddedSuccessfully()
        {
            // Arrange
            DataAccess.InitializeDatabase(); // Initialisez la base de données avant d'ajouter une note
            string titre = "Titre";
            string description = "Description";

            // Act
            DataAccess.AddNote(titre, description);

            // Assert
            // Vérifiez si vous pouvez récupérer la note ajoutée
            var note = DataAccess.GetNote(1); // Assurez-vous d'avoir ajouté une seule note
            Assert.NotNull(note);
            Assert.Equal(titre, note[0]);
            Assert.Equal(description, note[1]);
        }
    }
}
