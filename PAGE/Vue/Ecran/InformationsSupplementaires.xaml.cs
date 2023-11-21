using DocumentFormat.OpenXml.Drawing;
using PAGE.Model;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
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
        private Etudiants etudiants;
        private Notes notes;
        private SEXE sexeSelectionne;
        private bool estBoursier;
        private REGIME regimeEtu;
        private GROUPE groupeEtu;

        /// <summary>
        /// Constructeur qui prend l'étudiant selectionné avec le double clique
        /// </summary>
        /// <param name="EtudiantActuel">etudiant actuel</param>
        /// <author>Yamato & Laszlo & Nordine</author>
        public InformationsSupplementaires(Etudiant EtudiantActuel, Etudiants etudiants)
        {
            InitializeComponent();
            etudiant = EtudiantActuel;
            this.etudiants = etudiants;
            ChargerInfosImpEtudiant();
            ChargerInfosCompEtudiant();

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

            txtMail.Text = etudiant.Mail;

            //Radio boutton sexe
            switch (etudiant.Sexe)
            {
                case SEXE.FEMININ:
                    radioFemme.IsChecked = true;
                    break;
                case SEXE.MASCULIN:
                    radioHomme.IsChecked = true;
                    break;
                case SEXE.AUTRE:
                    radioAutre.IsChecked = true;
                    break;
            }


            txtTypebac.Text = etudiant.TypeBac;

            //boursier
            if (etudiant.EstBoursier)
                radioBoursierTrue.IsChecked = true;
            else
                radioBoursierFalse.IsChecked= true;

            //Charge la combobox de regime
            switch (etudiant.TypeFormation)
            {
                case REGIME.FI:
                    comboBoxRegime.SelectedIndex = 1;
                    break;
                case REGIME.FA:
                    comboBoxRegime.SelectedIndex = 2;
                    break;
                case REGIME.FC:
                    comboBoxRegime.SelectedIndex = 0;
                    break;
            }

            switch (etudiant.Groupe)
            {
                case GROUPE.A1:
                    comboBoxGroupe.SelectedIndex = 0;
                    break;
                case GROUPE.A2:
                    comboBoxGroupe.SelectedIndex = 1;
                    break;
                case GROUPE.B1:
                    comboBoxGroupe.SelectedIndex = 2;
                    break;
                case GROUPE.B2:
                    comboBoxGroupe.SelectedIndex = 3;
                    break;
                case GROUPE.C1:
                    comboBoxGroupe.SelectedIndex = 4;
                    break;
                case GROUPE.C2:
                    comboBoxGroupe.SelectedIndex = 5;
                    break;
                case GROUPE.D1:
                    comboBoxGroupe.SelectedIndex = 6;
                    break;
                case GROUPE.D2:
                    comboBoxGroupe.SelectedIndex = 7;
                    break;
                case GROUPE.E1:
                    comboBoxGroupe.SelectedIndex = 8;
                    break;
                case GROUPE.E2:
                    comboBoxGroupe.SelectedIndex = 9;
                    break;
            }
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
        /// Rend visible les informations complétementaires lors du clique sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Luvas</author>
        private void InfosComp_Click(object sender, RoutedEventArgs e)
        {
            if (contInfosComp.Visibility == Visibility.Collapsed)
            {
                contInfosComp.Visibility = Visibility.Visible;
                BoutonInfoComp.Visibility = Visibility.Collapsed;
                BoutonCacherInfoComp.Visibility = Visibility.Visible;

                if(etudiant.TelFixe == 0)
                {
                    txtTelFixe2.Text = "";
                }

                if (etudiant.TelPortable == 0)
                {
                    txtTelPortable2.Text = "";
                }
            }
        }

        /// <summary>
        /// Rend Invisible les informations complétementaires lors du clique sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Luvas</author>
        private void HideInfosComp_Click(object sender, RoutedEventArgs e)
        {

            if (contInfosComp.Visibility == Visibility.Visible)
            {
                contInfosComp.Visibility = Visibility.Collapsed;
                BoutonInfoComp.Visibility = Visibility.Visible;
                BoutonCacherInfoComp.Visibility = Visibility.Collapsed;
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
            BoutonModifier.Visibility = Visibility.Collapsed;
            BoutonValider.Visibility = Visibility.Visible;
            BoutonCreernote.IsEnabled = false;
            BoutonCreernote.Background = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Désactive le mode d'édition, empêchant l'utilisateur de modifier les informations et mets a jour l'étudiant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas / Nordine</author>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            BoutonValider.Visibility = Visibility.Collapsed;
            BoutonModifier.Visibility = Visibility.Visible;
            BoutonCreernote.IsEnabled = true;
            BoutonCreernote.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3DA79D"));

            DesactiverInput();

            //On récupere les nouvelles de l'étudiant
            Etudiant updateEtu = GetEtudiantUpdated();
            //On l'ajoute (le mets a jour puisqu'il existe)
            EtuDAO dao = new EtuDAO();
            dao.AddEtudiant(updateEtu);
            etudiants.UpdateEtu(etudiant);
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
                int.Parse(txtNumApogee.Text), txtName.Text, txtPrenom.Text, sexeSelectionne, txtTypebac.Text, txtMail.Text, groupeEtu, estBoursier,
                regimeEtu, txtDateNaissance2.SelectedDate.Value, txtLogin2.Text,
                telFixe, telPortable, txtAdresse2.Text);
            }
            return etudiantUpdated;
        }

        /// <summary>
        /// Quand on change le regime de la combobox, change la valeur du régime de l'etudiant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo</author>
        private void ComboBoxRegime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxRegime.SelectedIndex)
            {
                case 0:
                    regimeEtu = REGIME.FI;
                    break;
                case 1:
                    regimeEtu = REGIME.FC;
                    break;
                case 2:
                    regimeEtu = REGIME.FA;
                    break;
            }

        }

        /// <summary>
        /// Quand on change le groupe de la combobox, change la valeur du groupe de l'etudiant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo</author>
        private void ComboBoxGroupe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxGroupe.SelectedIndex)
            {
                case 0:
                    groupeEtu = GROUPE.A1;
                    break;
                case 1:
                    groupeEtu = GROUPE.A2;
                    break;
                case 2:
                    groupeEtu = GROUPE.B1;
                    break;
                case 3:
                    groupeEtu = GROUPE.B2;
                    break;
                case 4:
                    groupeEtu = GROUPE.C1;
                    break;
                case 5:
                    groupeEtu = GROUPE.C2;
                    break;
                case 6:
                    groupeEtu = GROUPE.D1;
                    break;
                case 7:
                    groupeEtu = GROUPE.D2;
                    break;
                case 8:
                    groupeEtu = GROUPE.E1;
                    break;
                case 9:
                    groupeEtu = GROUPE.E2;
                    break;
            }
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
                PopUp popUp = new PopUp("Création", "Le numéro d'apogée doit contenir uniquement des chiffres (maximum 8 chiffres)", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtNumApogee.Text))
            {
                PopUp popUp = new PopUp("Création", "Veuillez saisir un numéro apogée", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                PopUp popUp = new PopUp("Création", "Le champ Nom ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                PopUp popUp = new PopUp("Création", "Le champ Prénom ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtTypebac.Text))
            {
                PopUp popUp = new PopUp("Création", "Le champ de Type de Bac ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtMail.Text) || !txtMail.Text.Contains("@"))
            {
                PopUp popUp = new PopUp("Création", "Le champ e-mail ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog(); saisiCorrect = false;
            }
            else if (comboBoxGroupe.SelectedIndex == -1)
            {
                PopUp popUp = new PopUp("Création", "Le champ Groupe ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }
            else if (comboBoxRegime.SelectedIndex == -1)
            {
                PopUp popUp = new PopUp("Création", "Le champ Régime ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (txtDateNaissance2.SelectedDate.HasValue)
            {
                DateTime dateNaissance = txtDateNaissance2.SelectedDate.Value;
                DateTime dateActuelle = DateTime.Now;
                int age = dateActuelle.Year - dateNaissance.Year;

                if (age < 13)
                {
                    PopUp popUp = new PopUp("Création", "L'âge de l'étudiant doit être d'au moins 15 ans", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                    saisiCorrect = false;
                }
            }

            else if (!txtDateNaissance2.SelectedDate.HasValue)
            {
                PopUp popUp = new PopUp("Création", "Veuillez saisir une date de naissance", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if ((txtTelFixe2.Text == null) && !int.TryParse(txtTelFixe2.Text, out _))
            {
                PopUp popUp = new PopUp("Création", "Le numéro de téléphone fixe ne peut contenir que des chiffres", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if ((txtTelPortable2.Text == null) && !int.TryParse(txtTelPortable2.Text, out _))
            {
                PopUp popUp = new PopUp("Création", "Le numéro de téléphone portable ne peut contenir que des chiffres", TYPEICON.ERREUR);
                popUp.ShowDialog();
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

            // Active l'édition des comboboxes de regime et groupe
            comboBoxRegime.IsEnabled = true;
            comboBoxGroupe.IsEnabled = true;
        }

        /// <summary>
        /// Les TextBox sont rendus en lecture seule, les boutons radio sont désactivés, et les TextBox dans les WrapPanels ne sont plus éditables.
        /// La date de naissance est également en lecture seule.
        /// </summary>
        /// <author>Lucas / Nordine</author>
        private void DesactiverInput()
        {
            

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

            //rend les comboboxes de regime et groupe en lecture seule
            comboBoxRegime.IsEnabled = false;
            comboBoxGroupe.IsEnabled = false;
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
                    resultat = Resultats;
                    break;
                case 3: // "pour les resultats"
                    resultat = Orientation;
                    break;
                case 4: // "pour les orientation"
                    resultat = Medical;
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
        /// <author>Stephane/ Laszlo</author>
        private bool Absenteisme(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.ABSENTEISME);
        }

        /// <summary>
        /// La fonction utilise la catégorie Personnel  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par raison Personnel</returns>
        /// <author>Stephane/ Laszlo</author>
        private bool Personnel(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.PERSONNEL);
        }

        /// <summary>
        /// La fonction utilise la catégorie Médical  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par raison Médical</returns>
        /// <author>Stephane/ Laszlo</author>
        private bool Medical(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.MEDICAL);
        }

        /// <summary>
        /// La fonction utilise la catégorie Résultats  pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par Résultats</returns>
        /// <author>Stephane/ Laszlo</author>
        private bool Resultats(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.RESULTATS);
        }

        /// <summary>
        /// La fonction utilise la catégorie orientation pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        ///<returns>renvoie le filtre par orientation</returns>
        /// <returns></returns>
        /// <author>Stephane/ Laszlo</author>
        private bool Orientation(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.ORIENTATION);
        }

        /// <summary>
        /// La fonction utilise la catégorie Autre pour filtrer les Notes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>renvoie le filtre par Autre</returns>
        /// <author>Stephane/ Laszlo</author>
        private bool Autre(object obj)
        {
            var Filterobj = obj as Note;
            return Filterobj.Categorie.Equals(CATEGORIE.AUTRE);
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
            NoteDAO dao = new NoteDAO();
            this.notes = new Notes((await dao.GetAllNotesByApogee(etudiant.NumApogee)).ToList());

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
                    NoteDAO dao = new NoteDAO();
                    dao.DeleteNote(noteSelectionne);
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
                    // Créez une instance de la fenêtre CreationNote en passant la note et notes
                    CreationNote affichageNote = new CreationNote(noteSelectionne,this.notes,true);
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
            if (notes != null)
            {
                CreationNote creernote = new CreationNote(new Note(CATEGORIE.AUTRE, DateTime.Now, NATURE.AUTRE, "", etudiant.NumApogee), this.notes, false);
                creernote.Show();
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("Veuillez attendre la fin du chargement des notes", "Une erreur est survenue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
