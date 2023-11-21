using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using PAGE.Model;
using PAGE.Stockage;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetreCreerEtudiant.xaml
    /// </summary>
    public partial class FenetreCreerEtudiant : Window
    {
        private Etudiants etudiants;
        private SEXE sexeSelectionne;
        private bool estBoursier;
        private REGIME regimeEtu = REGIME.FI;
        private GROUPE groupeEtu = GROUPE.A1;

        /// <summary>
        /// Constructeur (initialiser le sexe à AUTRE  et le bool boursier a false
        /// </summary>
        /// <author>Nordine</author>
        public FenetreCreerEtudiant(Etudiants etudiants)
        {
            InitializeComponent();

            sexeSelectionne = SEXE.AUTRE;
            estBoursier = false;
            this.etudiants = etudiants;
        }


        /// <summary>
        /// Quand on clique sur créer un étudiant, vérifie si la saisis est cohérente, si oui créer l'étudiant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void CreerEtudiant(object sender, RoutedEventArgs e)
        {
            if (IsSaisiCorrect())
            {
                long telFixe = 0;
                long telPortable = 0;
                long.TryParse(txtTelFixe2.Text, out telFixe);
                long.TryParse(txtTelPortable2.Text, out telPortable);

        
                //on créer l'étudiant a partir des infos saisis dans la fenêtre
                Etudiant etudiant = new Etudiant(
                int.Parse(txtNumApogee.Text), txtName.Text, txtPrenom.Text, sexeSelectionne, txtTypebac.Text, txtMail.Text, groupeEtu, estBoursier,
                regimeEtu, txtDateNaissance2.SelectedDate.Value, txtLogin2.Text,
                telFixe, telPortable, txtAdresse2.Text);

                //on ajoute l'étudiant à la bdd
                EtuDAO dao = new EtuDAO();
                dao.AddEtudiant(etudiant);
                etudiants.AddEtu(etudiant);
            }

        }


        /// <summary>
        /// Verifie toutes les conditions nécessaires de la saisis de l'utilisateur pour une création d'étudiant sans erreur
        /// </summary>
        /// <returns>Si la saisi de l'utilisateur rempli toutes les conditions pour être valide</returns>
        /// <author>Nordine/ Laszlo</author>
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

            else if (comboBoxGroupe.SelectedIndex == -1)
            {
                MessageBox.Show("Le champ Groupe ne peut pas être vide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saisiCorrect = false;
            }

            else if (comboBoxRegime.SelectedIndex == -1)
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

            else if ((txtTelFixe2.Text== null) && !int.TryParse(txtTelFixe2.Text, out _))
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
    }
}
