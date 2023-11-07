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
    }
}
