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
        public InformationsSupplementaires()
        {
            InitializeComponent();
            ChargerInfosEtudiant(e);
        }

        public void ChargerInfosEtudiant()
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
    }
}
