
using PAGE.Model;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;
using TextBox = System.Windows.Controls.TextBox;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour InformationsSupplementaires.xaml
    /// </summary>
    public partial class InformationsSupplementaires : Window, IObservateur
    {
        private Etudiant etudiant;

        private Notes notes;
        private SEXE sexeSelectionne;
        private bool estBoursier;

        /// <summary>
        /// Constructeur qui prend l'étudiant selectionné avec le double clique
        /// </summary>
        /// <param name="EtudiantActuel">etudiant actuel</param>
        /// <author>Yamato & Laszlo & Nordine</author>
        public InformationsSupplementaires(Etudiant EtudiantActuel)
        {
            InitializeComponent();
            etudiant = EtudiantActuel;
            ChargerInfosImpEtudiant();

            sexeSelectionne = etudiant.Sexe;
            estBoursier = etudiant.EstBoursier;

            //on charge les notes de l'étudiant
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
            //On récupere les nouvelles de l'étudiant
            Etudiant updateEtu = GetEtudiantUpdated();
            //On l'ajoute (le mets a jour puisqu'il existe)
            EtuDAO.Instance.AddEtudiant(updateEtu);
        }

        /// <summary>
        /// Renvoi l'étudiant avec les nouvelles informations saisis par l'utilisateur
        /// </summary>
        /// <returns>l'étudiant modifié</returns>
        private Etudiant GetEtudiantUpdated()
        {
            Etudiant etudiantUpdated = null;
            if (IsSaisiCorrect())
            {
                long telFixe = 0;
                long telPortable = 0;
                long.TryParse(txtTelFixe2.Text, out telFixe);
                long.TryParse(txtTelPortable2.Text, out telPortable);
                //on créer l'étudiant a partir des infos saisis dans la fenêtre
                etudiantUpdated = new Etudiant(
                int.Parse(txtNumApogee.Text), txtName.Text, txtPrenom.Text, sexeSelectionne, txtTypebac.Text, txtMail.Text, txtGroupe.Text, estBoursier,
                txtRegime.Text, txtDateNaissance2.SelectedDate.Value, txtLogin2.Text,
                telFixe, telPortable, txtAdresse2.Text);
            }
            return etudiantUpdated;
        }
        #region Verification modifiaction étudiant
        /// <summary>
        /// Verifie toutes les conditions nécessaires de la saisis de l'utilisateur pour une modification d'étudiant sans erreur
        /// </summary>
        /// <returns>Si la saisi de l'utilisateur rempli toutes les conditions pour être valide</returns>
        /// <author>Nordine</author>
        private bool IsSaisiCorrect()
        {
            bool saisiCorrect = true;
            if ((!string.IsNullOrWhiteSpace(txtNumApogee.Text)) && !System.Text.RegularExpressions.Regex.IsMatch(txtNumApogee.Text, "^[0-9]{1,8}$"))
            {
                MessageBox.Show("Le numéro d'apogée doit contenir uniquement des chiffres (maximum 8 chiffres).", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtNumApogee.Text))
            {
                MessageBox.Show("Veuillez saisir un numéro apogée.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Le champ Nom ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Le champ Prénom ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtTypebac.Text))
            {
                MessageBox.Show("Le champ Type de Bac ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtMail.Text) || !txtMail.Text.Contains("@"))
            {
                MessageBox.Show("Le champ E-mail est vide ou invalide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtGroupe.Text))
            {
                MessageBox.Show("Le champ Groupe ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (string.IsNullOrWhiteSpace(txtRegime.Text))
            {
                MessageBox.Show("Le champ Régime ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if (txtDateNaissance2.SelectedDate.HasValue)
            {
                DateTime dateNaissance = txtDateNaissance2.SelectedDate.Value;
                DateTime dateActuelle = DateTime.Now;
                int age = dateActuelle.Year - dateNaissance.Year;
                if (age < 13)
                {
                    MessageBox.Show("L'âge de l'étudiant doit être d'au moins 15 ans.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    saisiCorrect = false;
                }
            }
            else if (!txtDateNaissance2.SelectedDate.HasValue)
            {
                MessageBox.Show("Veuillez saisir une date de naissance", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if ((txtTelFixe2.Text == null) && !int.TryParse(txtTelFixe2.Text, out _))
            {
                MessageBox.Show("Le numéro de téléphone fixe ne peut contenir que des chelse iffres.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            else if ((txtTelPortable2.Text == null) && !int.TryParse(txtTelPortable2.Text, out _))
            {
                MessageBox.Show("Le numéro de téléphone portable ne peut contenir que des chelse iffres.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }
            return saisiCorrect;
        }
        /// <summary>
        /// Change le sexe sélectionné à Féminin quand on clique sur le bouton radio "Femme"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void radioFemme_Checked(object sender, RoutedEventArgs e)
        {
            sexeSelectionne = SEXE.FEMININ;
        }

        /// <summary>
        /// Change le sexe sélectionné à Masculin quand on clique sur le bouton radio "Homme"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void radioHomme_Checked(object sender, RoutedEventArgs e)
        {
            sexeSelectionne = SEXE.MASCULIN;
        }
        /// <summary>
        /// Change le sexe sélectionné à Autre quand on clique sur le bouton radio "Autre"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void radioAutre_Checked(object sender, RoutedEventArgs e)
        {
            sexeSelectionne = SEXE.AUTRE;
        }
        /// <summary>
        /// Passe le bool estBoursier à faux quand on clique sur le radio bouton "Non"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void radioBoursierFalse_Checked(object sender, RoutedEventArgs e)
        {
            estBoursier = false;
        }
        /// <summary>
        /// Passe le bool estBoursier à vrai quand on clique sur le radio bouton "Oui"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void radioBoursierTrue_Checked(object sender, RoutedEventArgs e)
        {
            estBoursier = true;
        }
        #endregion


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
                //sauf le numero apogee
                if (tx.Name != "txtNumApogee")
                {
                    tx.IsReadOnly = false;
                    tx.BorderThickness = new Thickness(1);
                }
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
            this.notes = new Notes((await EtuDAO.Instance.GetAllNotesByApogee(etudiant.NumApogee)).ToList());

            foreach (Note note in notes.ListeNotes) 
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if (!maListViewNote.Items.Contains(note))
                    maListViewNote.Items.Add(note);
            }

            //On enregistre cette fenetre comme observeur des notes
            notes.Register(this);

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
                    notes.RemoveNote(noteSelectionne);

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
                    CreationNote affichageNote = new CreationNote(noteSelectionne,this.notes);
                    affichageNote.Show();
                }
            }
        }

        /// <summary>
        /// Crée une note pour un étudiant quand on clique sur le bouton créer et on initialise les valeurs nulles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void Creer_Click(object sender, RoutedEventArgs e)
        {
            CreationNote creernote = new CreationNote(new Note("",DateTime.Now,"","",etudiant.NumApogee), this.notes);
            creernote.Show();
            ChargementDiffereNotes();
        }

        /// <summary>
        /// est notifié par l'observeur pour pouvoir ctualiser la liste de notes
        /// </summary>
        /// <param name="Message">Message de notification</param>
        /// <author>Laszlo & Nordine</author>
        public async void Notifier(string Message)
        {
            // Attendre 2 secondes
            await Task.Delay(1000);

            ChargementDiffereNotes();
        }

    }
}
