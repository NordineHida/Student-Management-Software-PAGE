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
    /// Logique d'interaction pour AjoutAnnee.xaml
    /// </summary>
    public partial class AjoutAnnee : Window
    {
        /// <summary>
        /// Renvoie l'année saisie 
        /// </summary>
        public string AnneeSaisie
        {
            get { return txtAnneeSaisie.Text; }
        }
        public AjoutAnnee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ferme la fenêtre lors du click sur le bouton OK
        /// </summary>
        /// <author>Yamato</author>
        private void OkClick(object sender, RoutedEventArgs e)
        {
            // L'année n'est pas valide donc on met un message d'erreur
            if (EstAnneeSuperieure())
            {
                PopUp popUp = new PopUp("Année Incorrect", "L'année saisie ne peut être supérieur à l'année en cours", TYPEICON.ERREUR);
                popUp.ShowDialog();
            }
            // L'année est valide, donc on ferme la fenêtre
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Permet de savoir si l'année saisie est supérieur à l'année actuelle
        /// </summary>
        /// <returns>true si l'année est supérieur</returns>
        private bool EstAnneeSuperieure()
        {
            // Obtention de l'année actuelle
            int anneeActuelle = DateTime.Now.Year;
            bool estAnneeSuperieure = false;
            // Convertir la saisie en un nombre entier
            if (int.TryParse(AnneeSaisie, out int anneeSaisie))
            {
                // Comparez avec l'année actuelle
                estAnneeSuperieure = anneeSaisie > anneeActuelle;
            }
            // Gérez le cas où la saisie n'est pas un nombre valide
            return estAnneeSuperieure;
        }
    }
}
