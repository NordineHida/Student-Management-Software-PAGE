using System;
using System.Windows;
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
                int.Parse(txtNumApogee.Text), txtName.Text, txtPrenom.Text, sexeSelectionne, txtTypebac.Text, txtMail.Text, txtGroupe.Text, estBoursier,
                txtRegime.Text, txtDateNaissance2.SelectedDate.Value, txtLogin2.Text,
                telFixe, telPortable, txtAdresse2.Text);

                //on ajoute l'étudiant à la bdd
                EtuDAO.Instance.AddEtudiant(etudiant);
                etudiants.AddEtu(etudiant);
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

            else if (string.IsNullOrWhiteSpace(txtGroupe.Text))
            {
                PopUp popUp = new PopUp("Création", "Le champ Groupe ne peut pas être vide", TYPEICON.ERREUR);
                popUp.ShowDialog();
                saisiCorrect = false;
            }

            else if (string.IsNullOrWhiteSpace(txtRegime.Text))
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

            else if ((txtTelFixe2.Text== null) && !int.TryParse(txtTelFixe2.Text, out _))
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

    }
}
