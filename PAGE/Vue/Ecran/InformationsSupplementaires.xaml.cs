using DocumentFormat.OpenXml.ExtendedProperties;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        /// <author>Yamato / Lucas / Nordine</author>
        public void ChargerInfosImpEtudiant()
        {
            txtName.Text = etudiant.Nom;
            txtPrenom.Text = etudiant.Prenom;
            txtNumApogee.Text = etudiant.NumApogee.ToString();
            txtGroupe.Text = etudiant.Groupe;
            txtMail.Text = etudiant.Mail;

            //Radio boutton sexe
            switch (etudiant.Sexe)
            {
                case SEXE.FEMININ:
                    radioFemme.IsChecked = true;
                    break;
                case SEXE.MASCULIN:
                    radioFemme.IsChecked = true;
                    break;
                case SEXE.AUTRE:
                    radioFemme.IsChecked = true;
                    break;
            }


            txtTypebac.Text = etudiant.TypeBac;

            //boursier
            if (etudiant.EstBoursier)
                radioBoursierTrue.IsChecked = true;
            else
                radioBoursierFalse.IsChecked= true;


            txtRegime.Text = etudiant.TypeFormation;
        }

        /// <summary>
        /// Charge les informations complémentaires de l'étudiant
        /// </summary>
        /// <author>Yamato / Lucas / Nordine</author>
        public void ChargerInfosCompEtudiant()
        {
            txtDateNaissance2.SelectedDate = etudiant.DateNaissance;

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
                BoutonInfoComp.Content = "Cacher les informations complémentaires";
            }
            else
            {
                contInfosComp.Visibility = Visibility.Collapsed;
                BoutonInfoComp.Content = "Afficher les informations complémentaires";

            }
        }
        /// <summary>
        /// / Active le mode d'édition, permettant à l'utilisateur de modifier les informations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas / Nordine</author>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            ActiverInput();
        }

        /// <summary>
        /// Désactive le mode d'édition, empêchant l'utilisateur de modifier les informations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas / Nordine</author>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            DesactiverInput();
        }

        /// <summary>
        /// Les TextBox deviennent éditables, les boutons radio sont activés, et les TextBox dans les WrapPanels peuvent être édités.
        /// La date de naissance est également éditable.
        /// </summary>
        /// <author>Lucas / Nordine</author>
        private void ActiverInput()
        {
            BoutonValider.Visibility = Visibility.Visible;

            // Rend les TextBox éditables
            foreach (TextBox tx in GridInfoSupp.Children.OfType<TextBox>())
            {
                tx.IsReadOnly = false;
                tx.BorderThickness = new Thickness(1) ;
            }

            // Active les boutons radio pour le sexe
            foreach (RadioButton rb in RadioSexe.Children.OfType<RadioButton>())
            {
                rb.IsEnabled = true;
            }

            // Active les boutons radio pour le statut boursier
            foreach (RadioButton rb in RadioBoursier.Children.OfType<RadioButton>())
            {
                rb.IsEnabled = true;
            }

            // Rend éditables les TextBox dans les WrapPanels
            foreach (WrapPanel wp in contInfosComp.Children.OfType<WrapPanel>())
            {
                foreach (TextBox tx in wp.Children.OfType<TextBox>())
                {
                    tx.IsReadOnly = false;
                    tx.BorderThickness = new Thickness(1);
                }
            }
            // Active l'édition de la date de naissance
            txtDateNaissance2.IsEnabled = true;

        }

        /// <summary>
        /// Les TextBox sont rendus en lecture seule, les boutons radio sont désactivés, et les TextBox dans les WrapPanels ne sont plus éditables.
        /// La date de naissance est également en lecture seule.
        /// </summary>
        /// <author>Lucas / Nordine</author>
        private void DesactiverInput()
        {
            BoutonValider.Visibility = Visibility.Collapsed;

            // Rend les TextBox en lecture seule
            foreach (TextBox tx in GridInfoSupp.Children.OfType<TextBox>())
            {
                tx.IsReadOnly = true;
                tx.BorderThickness = new Thickness(0);
            }

            // Désactive les boutons radio pour le sexe
            foreach (RadioButton rb in RadioSexe.Children.OfType<RadioButton>())
            {
                rb.IsEnabled = false;
            }

            // Désactive les boutons radio pour le statut boursier
            foreach (RadioButton rb in RadioBoursier.Children.OfType<RadioButton>())
            {
                rb.IsEnabled = false;
            }

            // Rend les TextBox dans les WrapPanels en lecture seule
            foreach (WrapPanel wp in contInfosComp.Children.OfType<WrapPanel>())
            {
                foreach (TextBox tx in wp.Children.OfType<TextBox>())
                {
                    tx.IsReadOnly = true;
                    tx.BorderThickness = new Thickness(0);
                }
            }

            // Rend la date de naissance en lecture seule
            txtDateNaissance2.IsEnabled = false;

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
                maListViewNote.Items.Filter = null;
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
                maListViewNote.Items.Filter = null;
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
        /// Et utilisé quand la sélection de l'élément change.
        /// Elle met à jour le filtre appliqué fonction du choix de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stephane</author>
        private void CategorieCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Applique le filtre approprié dans maListView en fonction du choix de l'utilisateur.
            maListViewNote.Items.Filter = GetFilter();

        }

        /// <summary>
        /// Et utilisé quand la sélection de l'élément change.
        /// Elle met à jour le filtre appliqué fonction du choix de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stephane</author>
        private void ConfidentielCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Applique le filtre approprié dans maListView en fonction du choix de l'utilisateur.
            maListViewNote.Items.Filter = GetFilter();

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
                    resultat = Absenteisme;
                    break;
                case 1: // "pour les raisons personnels"
                    resultat = Personnel;
                    break;
                case 2: // "pour les raisons medical"
                    resultat = Medical;
                    break;
                case 3: // "pour les resultats"
                    resultat = Resultats;
                    break;
                case 4: // "pour les orientation"
                    resultat = Orientation;
                    break;
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
        private bool Absenteisme(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains("Absentéisme", StringComparison.OrdinalIgnoreCase);
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
            return Filterobj.Categorie.Contains("Personnel", StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// La fonction utilise la catégorie Médical  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par raison Médical</returns>
        /// <author>Stephane</author>
        private bool Medical(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains("Médical", StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// La fonction utilise la catégorie Résultats  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par Résultats</returns>
        /// <author>Stephane</author>
        private bool Resultats(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains("Résultats", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// La fonction utilise la catégorie orientation pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        ///<returns>renvoie le filtre par orientation</returns>
        /// <returns></returns>
        private bool Orientation(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Contains("Orientation", StringComparison.OrdinalIgnoreCase);
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
            return Filterobj.Categorie.Contains("Autre", StringComparison.OrdinalIgnoreCase);
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

        /// <summary>
        /// Supprime une note via l'API
        /// </summary>
        /// <author>Laszlo</author>
        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            if (maListViewNote.SelectedItem != null)
            {
                // Obtenez l'étudiant sélectionné dans la ListView
                Note noteSelectionne = maListViewNote.SelectedItem as Note;
                if (noteSelectionne != null)
                {
                   EtuDAO.Instance.DeleteNote(noteSelectionne);
                }
            }
        }

        /// <summary>
        /// Ouvre une fenêtre affichant la note lorsqu'on double clique sur la note
        /// </summary>
        /// <author>Laszlo</author>
        private void maListView_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (maListViewNote.SelectedItem != null)
            {
                // Obtenez l'étudiant sélectionné dans la ListView
                Note noteSelectionne = maListViewNote.SelectedItem as Note;

                if (noteSelectionne != null)
                {
                    // Créez une instance de la fenêtre InformationsSupplementaires en passant l'étudiant sélectionné en paramètre
                    AffichageNote affichageNote = new AffichageNote(noteSelectionne);
                    affichageNote.Show();
                }
            }
        }
    }
}
