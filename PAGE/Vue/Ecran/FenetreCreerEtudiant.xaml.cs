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
            this.etudiants = etudiants;
            ReinitialisationChamps();
        }
        /// <summary>
        /// Cette méthode permet de réinitialiser les champs après la création d'un étudiant pour en créer un autre
        /// </summary>
        /// <author>Lucas</author>
        private void ReinitialisationChamps()
        {
            radioAutre.IsChecked = true;
            radioBoursierFalse.IsChecked = true;
            txtNumApogee.Text = "";
            txtName.Text = "";
            txtPrenom.Text = "";
            txtTypebac.Text = "";
            txtMail.Text = "";
            comboBoxGroupe.SelectedIndex = -1;

            comboBoxRegime.SelectedIndex = -1;

            txtLogin2.Text = "";
            txtAdresse2.Text = "";
            txtTelFixe2.Text = "";
            txtTelPortable2.Text = "";
            txtDateNaissance2.SelectedDate = DateTime.Now.AddYears(-15);
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
                dao.CreateEtu(etudiant);
                etudiants.AddEtu(etudiant);

                //on réinitialise la page
                ReinitialisationChamps();
            }

        }


        /// <summary>
        /// Verifie toutes les conditions nécessaires de la saisis de l'utilisateur pour une création d'étudiant sans erreur
        /// </summary>
        /// <returns>Si la saisi de l'utilisateur rempli toutes les conditions pour être valide</returns>
        /// <author>Nordine et Yamato</author>
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
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
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
                        PopUp popUp = new PopUp("Création", "L'âge de l'étudiant doit être d'au moins 15 ans", TYPEICON.ERREUR);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Creation", "The student must be at least 15 years old", TYPEICON.ERREUR);
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

            else if ((txtTelFixe2.Text== null) && !int.TryParse(txtTelFixe2.Text, out _))
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
