
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour InformationsSupplementaires.xaml
    /// </summary>
    public partial class InformationsSupplementaires : Window
    {
        private Etudiant etudiant;
        private List<Note> notes;

        /// <summary>
        /// Constructeur qui prend l'étudiant selectionné avec le double clique
        /// </summary>
        /// <param name="EtudiantActuel">etudiant actuel</param>
        /// <author>Yamato</author>
        public InformationsSupplementaires(Etudiant EtudiantActuel)
        {
            InitializeComponent();
            etudiant = EtudiantActuel;
            ChargerInfosImpEtudiant();
            ChargementDiffereNotes();
        }

        /// <summary>
        /// Charge les informations importantes de l'étudiant
        /// </summary>
        /// <author>Yamato</author>
        public void ChargerInfosImpEtudiant()
        {
            txtName.Text = etudiant.Nom;
            txtPrenom.Text = etudiant.Prenom;
            txtNumApogee.Text = etudiant.NumApogee.ToString();
            txtGroupe.Text = etudiant.Groupe;
            txtMail.Text = etudiant.Mail;
            txtSexe.Text = etudiant.Sexe.ToString();
            txtTypebac.Text = etudiant.TypeBac;
            txtBoursier.Text = etudiant.EstBoursier ? "Oui" : "Non";
            txtRegime.Text = etudiant.TypeFormation;
        }

        /// <summary>
        /// Charge les informations complémentaires de l'étudiant
        /// </summary>
        /// <author>Yamato</author>
        public void ChargerInfosCompEtudiant()
        {
            txtDateNaissance2.Text = etudiant.DateNaissance.ToString();
            txtAdresse2.Text = etudiant.Adresse;
            txtTelFixe2.Text = etudiant.TelFixe.ToString();
            txtTelPortable2.Text = etudiant.TelPortable.ToString();
            txtLogin2.Text = etudiant.Login;
            
        }


        /// <summary>
        /// Rend visible les informations complétementaires lors du clique sur le bouton ou les rend invisibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Yamato</author>
        private void InfosComp_Click(object sender, RoutedEventArgs e)
        {
            if (contInfosComp.Visibility == Visibility.Collapsed)
            {
                ChargerInfosCompEtudiant();
                contInfosComp.Visibility = Visibility.Visible;
            }
            else
            {
                contInfosComp.Visibility = Visibility.Collapsed;
                
            }
        }
        #region affichage trie note
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stéphane</author>

        private void ConfidentielCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Vérifie si la case à cocher est cochée
            if (ConfidentielCheckBox.IsChecked == true)
            {
                // Affiche la ComboBox si la case à cocher est cochée
                ConfidentielCombobox.Visibility = Visibility.Visible;
            }
        }
        // Vérifie si la case à cocher est cochée
        private void ConfidentielCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ConfidentielCheckBox.IsChecked == false)
            {
                // cache la ComboBox si la case à cocher n'est pas cochée
                ConfidentielCombobox.Visibility = Visibility.Hidden;
            }
        }
        // Vérifie si la case à cocher est cochée
        private void CategorieCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (CategorieCheckBox.IsChecked == true)
            {
                // Affiche la ComboBox si la case à cocher est cochée
                CategorieCombobox.Visibility = Visibility.Visible;
            }
        }
        // Vérifie si la case à cocher est cochée
        private void CategorieCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CategorieCheckBox.IsChecked == false)
            {
                // cache la ComboBox si la case à cocher n'est pas cochée

                CategorieCombobox.Visibility = Visibility.Hidden;
            }
        }






        /// <summary>
        /// trie la liste a partir de la liste cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stéphane</author>
        private bool isSortAscending = true;

        private void Trie_ClickNote(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);

            string sortBy = column.Tag.ToString();

            // Vérifie s'il existe des descriptions de tri pour les éléments de la liste.
            if (maListViewNote.Items.SortDescriptions.Count > 0)
            {
                // Efface les descriptions de tri existantes.
                maListViewNote.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir;


            // Détermine la direction de tri en fonction de la valeur de 'isSortAscending'.
            // Si 'isSortAscending' est vrai, le tri est défini sur croissant, sinon sur décroissant.
            // Inverse ensuite la valeur de 'isSortAscending'.
            if (isSortAscending)
            {
                newDir = ListSortDirection.Ascending; //trie croisant 
                isSortAscending = false;
            }
            else
            {
                newDir = ListSortDirection.Descending; //trie décroisant
                isSortAscending = true;
            }

            // Ajouter une nouvelle description de tri à la liste 
            maListViewNote.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }




        /// <summary>
        /// renvoi le filtre selctionner dans le combobox
        /// </summary>
        /// <returns>le filtre adapter</returns>
        /// <author>Stephane</author>
        private Predicate<object> GetFilter()
        {

            Predicate<object> resultat = null;

            switch (CategorieCombobox.SelectedIndex)
            {
                case 0: // "pour les raisons d'absences"
                    resultat = Absentéisme;
                    break;
                case 1: // "pour les raisons personnels"
                    resultat = Personnel;
                    break;
                case 2: // "pour les raisons médical"
                    resultat = Médical;
                    break;
                case 3: // "pour les résultats"
                    resultat = Résultats;
                    break;
                /*case 4: // "pour les orientation"
                    resultat = Orientation;
                    break;*/
                case 5: // "pour toutes les autres raisons"
                    resultat = Autre;
                    break;
            }

            return resultat;

        }
        /// <summary>
        /// La fonction utilise la catégorie Absentéisme pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        ///<returns>renvoie le filtre par Absentéisme</returns>
        /// <returns></returns>
        private bool Absentéisme(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains(CategorieCombobox.Text, StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// La fonction utilise la catégorie Personnel  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par raison Personnel</returns>
        /// <author>Stephane</author>
        private bool Personnel(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains(CategorieCombobox.Text, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// La fonction utilise la catégorie Médical  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par raison Médical</returns>
        /// <author>Stephane</author>
        private bool Médical(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains(CategorieCombobox.Text, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// La fonction utilise la catégorie Résultats  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par Résultats</returns>
        /// <author>Stephane</author>
        private bool Résultats(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains(CategorieCombobox.Text, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// La fonction utilise la catégorie Autre pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par Autre</returns>
        /// <author>Stephane</author>
        private bool Autre(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains(CategorieCombobox.Text, StringComparison.OrdinalIgnoreCase);
        }









        #endregion



        /// <summary>
        /// Chargement des notes différé via l'API
        /// </summary>
        /// <author>Laszlo</author>
        private async Task ChargementDiffereNotes()
        {
            //On reinitialise la liste
            maListViewNote.Items.Clear();

            //On récupere l'ensemble des étudiants via l'API
            this.notes = (await EtuDAO.Instance.GetAllNotesByApogee(etudiant.NumApogee)).ToList();

            foreach (Note note in notes)
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if (!maListViewNote.Items.Contains(note))
                    maListViewNote.Items.Add(note);
            }
        }
    }
}
