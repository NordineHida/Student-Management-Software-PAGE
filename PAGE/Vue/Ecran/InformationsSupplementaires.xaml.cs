using PAGE.Model;
using System;
using System.Collections.Generic;
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
            }
            else
            {
                contInfosComp.Visibility = Visibility.Collapsed;
                
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
                }
            }

            // Rend la date de naissance en lecture seule
            txtDateNaissance2.IsEnabled = false;
        }
    }
}
