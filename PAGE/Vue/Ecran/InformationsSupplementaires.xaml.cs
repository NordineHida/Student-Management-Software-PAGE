using DocumentFormat.OpenXml.Drawing;
using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
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
        private Promotion promo;
        private Token token;


        /// <summary>
        /// Constructeur qui prend l'étudiant selectionné avec le double clique
        /// </summary>
        /// <param name="EtudiantActuel">etudiant actuel</param>
        /// <author>Yamato & Laszlo & Nordine</author>
        public InformationsSupplementaires(Etudiant EtudiantActuel, Etudiants etudiants,Promotion promo, Token? tokenUtilisateur)
        {
            InitializeComponent();
            this.promo = promo;
            etudiant = EtudiantActuel;
            this.etudiants = etudiants;

            if (tokenUtilisateur != null )
            {
                this.token = tokenUtilisateur;
                if (token.UserToken.Roles.ContainsKey(promo.AnneeDebut))
                {
                    if (token.UserToken.Roles[promo.AnneeDebut] != ROLE.LAMBDA && token.UserToken.Roles[promo.AnneeDebut] != ROLE.ADMIN)
                    {
                        BoutonCreernote.Visibility = Visibility.Visible;
                        BoutonModifier.Visibility = Visibility.Visible;
                    }
                }
                   

            }

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
            //si il a une date de naissance
            if (etudiant.DateNaissance != DateTime.MinValue)
            {
                txtDateNaissance2.SelectedDate = etudiant.DateNaissance;
            }
            else
                txtDateNaissance2.SelectedDate = null ;

            txtAdresse2.Text = etudiant.Adresse;

            if (etudiant.TelFixe != 0)
            {
                txtTelFixe2.Text = etudiant.TelFixe.ToString();
            }
            else
                txtTelFixe2.Text = "";

            if (etudiant.TelPortable != 0)
            {
                txtTelPortable2.Text = etudiant.TelPortable.ToString();
            }
            else
                txtTelPortable2.Text = "";
            txtLogin2.Text = etudiant.Login;
            
        }


        /// <summary>
        /// Rend visible les informations complétementaires lors du clique sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
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
        /// <author>Lucas</author>
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
            BoutonAddMail.IsEnabled = true;
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
            dao.AddEtudiant(updateEtu,this.promo);
            etudiants.UpdateEtu(etudiant);
        }

        /// <summary>
        /// Renvoi l'étudiant avec les nouvelles informations saisis par l'utilisateur
        /// </summary>
        /// <returns>l'étudiant modifié</returns>
        /// <author>Laszlo</author>
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
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le numéro d'apogée doit contenir uniquement des chiffres (maximum 8 chiffres)", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The apogee number must contain only digits (maximum 8 digits)", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtNumApogee.Text))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Veuillez saisir un numéro apogée", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "Please enter an apogee number", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ Nom ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The Name field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ Prénom ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The First Name field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtTypebac.Text))
            {
                if(Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ de Type de Bac ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The A-Level Type field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtMail.Text) || !txtMail.Text.Contains("@"))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ e-mail ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The e-mail field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }
            else if (comboBoxGroupe.SelectedIndex == -1)
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ Groupe ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The Group field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }
            else if (comboBoxRegime.SelectedIndex == -1)
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ Régime ne peut pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The Plan field cannot be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if (txtDateNaissance2.SelectedDate.HasValue)
            {
                DateTime dateNaissance = txtDateNaissance2.SelectedDate.Value;
                DateTime dateActuelle = DateTime.Now;
                int age = dateActuelle.Year - dateNaissance.Year;

                if (age < 13)
                {
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Création", "L'âge de l'étudiant doit être d'au moins 13 ans", TYPEICON.ERREUR);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Creation", "The student must be at least 13 years old", TYPEICON.ERREUR);
                        popUp.ShowDialog();
                    }
                    saisiCorrect = false;
                }
            }

            else if (!txtDateNaissance2.SelectedDate.HasValue)
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Veuillez saisir une date de naissance", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "Please enter a date of birth", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if ((txtTelFixe2.Text == null) && !int.TryParse(txtTelFixe2.Text, out _))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le numéro de téléphone fixe ne peut contenir que des chiffres", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The landline telephone number can only contain digits", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                saisiCorrect = false;
            }

            else if ((txtTelPortable2.Text == null) && !int.TryParse(txtTelPortable2.Text, out _))
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le numéro de téléphone portable ne peut contenir que des chiffres", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "Mobile phone numbers can only contain digits", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
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

            //active le bouton pour ajouter un mail
            BoutonAddMail.IsEnabled = true;
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

            //desactive le bouton mail
            BoutonAddMail.IsEnabled = false;
        }


        /// <summary>
        /// Charge les notes depuis le DAO, les affiches et s'enregistre en observateur
        /// </summary>
        /// <returns></returns>
        /// <author>Laszlo/Nordine</author>
        private async Task ChargementDiffereNotes()
        {

            // On récupère l'ensemble des étudiants via l'API
            NoteDAO dao = new NoteDAO();
            this.notes = new Notes((await dao.GetAllNotesByApogee(etudiant.NumApogee)).ToList());

            // On enregistre cette fenêtre comme observateur des notes
            notes.Register(this);

            // Remplir le WrapPanel avec les NoteComponent
            RemplirWrapPanelNotes(notes.ListeNotes);
        }


        /// <summary>
        /// Remplit le WrapPanel des notes 
        /// </summary>
        /// <param name="notes">Liste de notes à afficher</param>
        /// <author>Nordine</author>
        private void RemplirWrapPanelNotes(List<Note> notes)
        {
            // Efface les anciens éléments
            WrapPanelNote.Children.Clear();

            //on trie les notes du plus recent au plus ancient
            notes = notes.OrderByDescending(note => note.DatePublication).ToList();

            foreach (Note note in notes)
            {
                // Si la note n'est pas déjà dans le WrapPanel, on l'y ajoute
                if (!WrapPanelNote.Children.OfType<NoteComponent>().Any(nc => nc.Note.IdNote == note.IdNote))
                { 
                    // Ajoute le NoteComponent personnalisé au WrapPanel
                    NoteComponent noteComponent = new NoteComponent(note);
                    WrapPanelNote.Children.Add(noteComponent);
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
                CreationNote creernote;
                if (this.token != null)
                {
                    creernote = new CreationNote(new Note(CATEGORIE.AUTRE, DateTime.Now, NATURE.AUTRE, "", etudiant.NumApogee, CONFIDENTIALITE.PUBLIC), this.notes, false,this.promo, token);
                }
                else
                {
                    creernote = new CreationNote(new Note(CATEGORIE.AUTRE, DateTime.Now, NATURE.AUTRE, "", etudiant.NumApogee, CONFIDENTIALITE.PUBLIC), this.notes, false,this.promo, null);
                }
                creernote.Show();
            }
            else
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Une erreur est survenue", "Veuillez attendre la fin du chargement des notes", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("An error has occurred", "Please wait until the notes have finished loading", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
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


        /// <summary>
        /// Ouvre une fenêtre affichant la note lorsqu'on double clique sur la note
        /// </summary>
        /// <author>Nordine</author>
        private void DoubleCliqueSurNote(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is NoteComponent noteComponent)
            {
                // On recupère l'étudiant associé au EtudiantComponent
                Note noteSelectionne = noteComponent.Note;

                if (noteSelectionne != null)
                {
                    // Créez une instance de la fenêtre CreationNote en passant la note et notes
                    CreationNote affichageNote;
                    if (this.token != null)
                    {
                        affichageNote = new CreationNote(noteSelectionne, this.notes, true,this.promo, token);
                    }
                    else
                    {
                        affichageNote = new CreationNote(noteSelectionne, this.notes, true,this.promo, null);
                    }
                    affichageNote.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Créer le mail de l'iut au format prenom.nom@iut-dijon.u-bourgogne.Fr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMailExtension(object sender, RoutedEventArgs e)
        {
            txtMail.Text = $"{txtPrenom.Text.ToLower()}.{txtName.Text.ToLower()}@iut-dijon.u-bourgogne.fr";
        }
    }
}
