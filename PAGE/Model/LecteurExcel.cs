using System;
using System.Collections.Generic;
using System.Linq;
using APIEtudiant.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PAGE.Model.Enumerations;

namespace PAGE.Model
{
    /// <summary>
    /// Lit le fichier excel avec les étudiants et renvoi une liste d'étudiant
    /// </summary>
    /// <author>Laszlo & Nordine</author>
    public class LecteurExcel
    {
        /// <summary>
        /// Renvoi les étudiants du fichier excel donnée
        /// </summary>
        /// <param name="pathExcel">chemin vers le fichier excel donnée</param>
        /// <returns>la liste des étudiants du fichier excel</returns>
        /// <author>Laszlo & Nordine</author>
        public IEnumerable<Etudiant> GetEtudiants(string pathExcel)
        {
            string apogee =null;
            string nom = null;
            string prenom = null;
            string sexe = null;
            string typeBac = null;
            string mail = null;
            string groupe = null;
            string estBoursier = null;
            string regimeFormation = null;
            string dateNaissance = null;
            string adresse = null;
            string telPortable=null;
            string telFixe = null;
            string login = null;

            int apogeeInt=-1;
            long telPortableInt=-1;
            long telFixeInt = -1;
            DateTime dateNaissanceDT = new DateTime();
            SEXE sexeEtu = SEXE.AUTRE;
            REGIME regimeEtu = REGIME.FI;
            GROUPE groupeEtu = GROUPE.A1;
            bool estBoursierBool = false;

            //Date de base d'Excel à partir de laquel on compte le nombre de jour
            DateTime baseDate = new DateTime(1900, 1, 1);

            List<Etudiant> etudiants= new List<Etudiant>();

            // Ouvre le document en lecture seule
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(pathExcel, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                int header = 0;

                foreach (Row row in sheetData.Elements<Row>())
                {
                    //on reset l'apogee
                    apogeeInt = -1;

                    //On enleve la ligne de header
                    header += 1;
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        //Si c'est pas le header alors on récupere les éléments
                        if (header > 1)
                        {
                            // Obtenez la valeur de la cellule
                            string cellValue = GetCellValue(cell, workbookPart);
                            if (cell.CellReference.Value.StartsWith("A") && !cell.CellReference.Value.StartsWith("AA")) apogee = cellValue;
                            if (cell.CellReference.Value.StartsWith("B")) groupe = cellValue;
                            if (cell.CellReference.Value.StartsWith("C")) estBoursier = cellValue;
                            if (cell.CellReference.Value.StartsWith("D")) regimeFormation = cellValue;
                            if (cell.CellReference.Value.StartsWith("E")) nom = cellValue;
                            if (cell.CellReference.Value.StartsWith("F")) prenom = cellValue;
                            if (cell.CellReference.Value.StartsWith("G")) sexe = cellValue;
                            if (cell.CellReference.Value.StartsWith("H")) dateNaissance = cellValue;
                            if (cell.CellReference.Value.StartsWith("N")) adresse = cellValue;
                            if (cell.CellReference.Value.StartsWith("S")) typeBac = cellValue;
                            if (cell.CellReference.Value.StartsWith("T")) mail = cellValue;
                            if (cell.CellReference.Value.StartsWith("W")) telPortable = cellValue;
                            if (cell.CellReference.Value.StartsWith("X")) telFixe = cellValue;
                            if (cell.CellReference.Value.StartsWith("AA"))login = cellValue;

                            // Convertit les chaîne en int                            
                            if (apogee != null)
                                apogeeInt = int.Parse(apogee);
                           
                            if (telPortable != null)
                                telPortableInt = long.Parse(telPortable);
                         
                            if (telFixe != null)
                                telFixeInt = long.Parse(telFixe);


                            //Conversion du string en SEXE
                            //Par défaut le sexe est autre
                            sexeEtu = SEXE.AUTRE;
                            switch (sexe)
                            {
                                case "F":
                                    sexeEtu = SEXE.FEMININ;
                                    break;
                                case "M":
                                    sexeEtu = SEXE.MASCULIN;
                                    break;
                            }

                            //Conversion du string en REGIME
                            switch (regimeFormation)
                            {
                                case "FI":
                                    regimeEtu = REGIME.FI;
                                    break;
                                case "FC":
                                    regimeEtu = REGIME.FC;
                                    break;
                                case "FA":
                                    regimeEtu = REGIME.FA;
                                    break;
                            }

                            //Conversion du string groupe en valeur de l'énumération GROUPE
                            switch (groupe)
                            {
                                case "A2":
                                    groupeEtu = GROUPE.A2;
                                    break;
                                case "B1":
                                    groupeEtu = GROUPE.B1;
                                    break;
                                case "B2":
                                    groupeEtu = GROUPE.B2;
                                    break;
                                case "C1":
                                    groupeEtu = GROUPE.C1;
                                    break;
                                case "C2":
                                    groupeEtu = GROUPE.C2;
                                    break;
                                case "D1":
                                    groupeEtu = GROUPE.D1;
                                    break;
                                case "D2":
                                    groupeEtu = GROUPE.D2;
                                    break;
                                case "E1":
                                    groupeEtu = GROUPE.E1;
                                    break;
                                case "E2":
                                    groupeEtu = GROUPE.E2;
                                    break;
                            }



                            //Conversion du string en Bool
                            estBoursierBool = false;
                            if (estBoursier == "OUI") estBoursierBool = true;

                            dateNaissanceDT = DateTime.MinValue;
                            //Conversion string en DateTime
                            if (dateNaissance!= null && dateNaissance != "")
                            {
                                // Ajoutez le nombre de jours à la date de base pour obtenir la date correcte
                                dateNaissanceDT = baseDate.AddDays(int.Parse(dateNaissance) - 2); // Soustrayez 2 jours pour corriger un décalage de 2 jours dans Excel
                                //On reinitialise la valeur de base de la date de référence d'Excel
                                baseDate = new DateTime(1900, 1, 1);
                            }



                        }
                    }

                    if (apogeeInt!= -1)
                    {
                        // Vérifie si un étudiant avec le même numéro Apogee existe déjà dans la liste
                        if (!etudiants.Any(e => e.NumApogee == apogeeInt))
                        {
                            // On crée l'étudiant
                            Etudiant etudiant = new Etudiant(apogeeInt, nom, prenom, sexeEtu, typeBac, mail, groupeEtu, estBoursierBool, regimeEtu, dateNaissanceDT, login, telPortableInt, telFixeInt, adresse);

                            // On l'ajoute à la liste d'étudiant
                            etudiants.Add(etudiant);
                        }
                    }
                }
                
            }
            return etudiants;
        }

        /// <summary>
        /// Renvoi la valeur d'une cellule
        /// </summary>
        /// <param name="cell">cellule à lire</param>
        /// <param name="workbookPart">feuille excel utilisé</param>
        /// <returns>le contenu de la cellule sous forme de string</returns>
        /// <author>Laszlo</author>
        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null)
            {
                return null;
            }

            string cellValue = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                var sharedStringTablePart = workbookPart.SharedStringTablePart;
                if (sharedStringTablePart != null)
                {
                    int index;
                    if (int.TryParse(cellValue, out index))
                    {
                        var sharedStringItem = sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(index);
                        if (sharedStringItem != null)
                        {
                            return sharedStringItem.Text.Text;
                        }
                    }
                }
            }
            return cellValue;
        }
    }
}
