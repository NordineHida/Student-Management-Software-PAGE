using Spire.Doc;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace PAGE.Model
{
    /// <summary>
    /// Genere le word avec les notes
    /// </summary>
    /// <author>Nordine</author>
    public class WordGenerateur
    {
        /// <summary>
        /// Génére le word avec toutes les notes passé
        /// </summary>
        /// <param name="notes">Liste des notes à mettre dans le word</param>
        /// <param name="promo">Promotion séléctionné</param>
        /// <author>Nordine</author>
        public static void GenererWord(Dictionary<string, IEnumerable<Note>> notes, Promotion promo)
        {
            string path = Parametre.Instance.PathGenerationWord;
            Spire.Doc.Document doc = new Spire.Doc.Document();

            foreach (var etudiantNotes in notes)
            {
                string etudiantKey = etudiantNotes.Key; // Nom Prenom
                IEnumerable<Note> etudiantNotesList = etudiantNotes.Value;

                // Ajouter le titre (Nom Prenom) au début de la section
                Spire.Doc.Section section = doc.AddSection();
                Paragraph titleParagraph = section.AddParagraph();
                TextRange titleTextRange = titleParagraph.AppendText(etudiantKey);
                titleTextRange.CharacterFormat.Bold = true;
                titleTextRange.CharacterFormat.FontSize = 14;
                titleTextRange.CharacterFormat.FontName = "Calibri";
                titleParagraph.Format.BeforeSpacing = 10; // Espace avant le tableau

                // Ajouter le tableau de notes après le titre
                Spire.Doc.Table table = section.AddTable(true);

                // En-tête du tableau
                string[] headers = { "Catégorie", "Date", "Nature", "Commentaire", "Titre" };
                table.ResetCells(etudiantNotesList.Count() + 1, headers.Length);
                TableRow headerRow = table.Rows[0];
                headerRow.IsHeader = true;
                headerRow.Height = 23;
                headerRow.RowFormat.BackColor = Color.LightSeaGreen;

                for (int i = 0; i < headers.Length; i++)
                {
                    Paragraph p = headerRow.Cells[i].AddParagraph();
                    headerRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = HorizontalAlignment.Center;

                    TextRange TR = p.AppendText(headers[i]);
                    TR.CharacterFormat.FontName = "Calibri";
                    TR.CharacterFormat.FontSize = 12;
                    TR.CharacterFormat.Bold = true;
                }

                // Ajout des données
                int rowIndex = 1;
                foreach (var note in etudiantNotesList)
                {
                    TableRow dataRow = table.Rows[rowIndex++];
                    dataRow.Height = 20;

                    string[] rowData = {
                note.Categorie.ToString(),
                note.DatePublication.ToShortDateString(),
                note.Nature.ToString(),
                note.Commentaire,
                note.Titre // Ajoutez cette ligne pour la colonne "Titre"
            };

                    for (int i = 0; i < rowData.Length; i++)
                    {
                        dataRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        Paragraph p2 = dataRow.Cells[i].AddParagraph();
                        TextRange TR2 = p2.AppendText(rowData[i]);
                        p2.Format.HorizontalAlignment = HorizontalAlignment.Center;

                        TR2.CharacterFormat.FontName = "Calibri";
                        TR2.CharacterFormat.FontSize = 11;
                    }
                }
            }

            doc.SaveToFile($"{path}/Note-{promo.NomPromotion}-{promo.AnneeDebut}-{promo.AnneeDebut + 1}.docx", FileFormat.Docx);
        }



    }
}
