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

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            BoutonValider.Visibility = Visibility.Visible;
            foreach (TextBox tx in GridInfoSupp.Children.OfType<TextBox>())
            {
                tx.IsReadOnly = false;
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            BoutonValider.Visibility = Visibility.Collapsed;
            foreach (TextBox tx in GridInfoSupp.Children.OfType<TextBox>())
            {
                tx.IsReadOnly = true;
            }

        }
    }
}
