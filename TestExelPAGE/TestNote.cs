using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPAGE
{
    /// <summary>
    /// Classe permettant de réaliser des tests unitaires sur les notes
    /// </summary>
    /// <author>Lucas</author>
    public class TestNote
    {
        /// <summary>
        /// Méthode permettant de tester la création d'une note
        /// </summary>
        /// <author>Lucas</author>

        [Fact]
        public void TestNoteConstructor()
        {
            CATEGORIE categorie = CATEGORIE.MEDICAL;
            DateTime datePublication = DateTime.Now;
            NATURE nature = NATURE.RDV;
            string commentaire = "Ceci est un commentaire.";
            int apogeeEtudiant = 123;

            Note note = new Note(categorie, datePublication, nature, commentaire, apogeeEtudiant);

            Assert.Equal(categorie, note.Categorie);
            Assert.Equal(datePublication, note.DatePublication);
            Assert.Equal(nature, note.Nature);
            Assert.Equal(commentaire, note.Commentaire);
            Assert.Equal(apogeeEtudiant, note.ApogeeEtudiant);
        }
    }
}
