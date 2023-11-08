using System;
using System.Windows;
using PAGE.Model;
using PAGE.Stockage;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetreCreerEtudiant.xaml
    /// </summary>
    public partial class FenetreCreerEtudiant : Window
    {

        private SEXE sexeSelectionne;
        private bool estBoursier;

        // VERIFIE SI DES ELEMENTS OBLIGATOIRE SONT BIEN RENTRER !!!!!!!!!!!!!!!!!!!!!!!!! LES MARQUES AVEC UNE PETITE ETOILE ROUGE COMME FORMULAIRE + verifier age min?
        public FenetreCreerEtudiant()
        {
            InitializeComponent();

            sexeSelectionne = SEXE.AUTRE;
            estBoursier = false;
        }


        private void CreerEtudiant(object sender, RoutedEventArgs e)
        {
            //FAIRE DES VERIFICATION DE SAISI ICI !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! SURMENT DANS UNE AUTRE METHODE  private bool IsSaisiCorrect
            //si la date de naissance à bien été saisi
            if (txtDateNaissance2.SelectedDate.HasValue)
            {
                //on créer l'étudiant a partir des infos saisis dans la fenêtre
                Etudiant etudiant = new Etudiant(
                    int.Parse(txtNumApogee.Text), txtName.Text, txtPrenom.Text, sexeSelectionne, txtTypebac.Text, txtMail.Text, txtGroupe.Text, estBoursier,
                    txtRegime.Text, txtDateNaissance2.SelectedDate.Value, txtLogin2.Text,
                    long.Parse(txtTelFixe2.Text), long.Parse(txtTelPortable2.Text), txtAdresse2.Text);

                //on ajoute l'étudiant à la bdd
                EtuDAO.Instance.AddEtudiant(etudiant);

            }
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
