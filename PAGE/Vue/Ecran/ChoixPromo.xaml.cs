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
    /// Logique d'interaction pour ChoixPromo.xaml
    /// </summary>
    public partial class ChoixPromo : Window
    {
        private List<Annee> annees = new List<Annee>();

        public ChoixPromo()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Clique bouton valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ValiderChoixPromo(object sender, RoutedEventArgs e)
        {
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal();
            fenetrePrincipal.Show();

            this.Close();
        }

        /// <summary>
        /// Bouton ouvrant une nouvelle fenetre permettant d'ajouter une année
        /// </summary>
        /// <author>Yamato</author>        
        private void AjouterAnnee(object sender, RoutedEventArgs e)
        {
            // On ouvre une nouvelle fenêtre permettant la saisir de l'année à ajouter
            AjoutAnnee ajoutAnnee = new AjoutAnnee();
            ajoutAnnee.ShowDialog();

            Annee nouvelleAnnee = new Annee(ajoutAnnee.AnneeSaisie, null, null, null);
            annees.Add(nouvelleAnnee);

            ComboBoxAnnee.ItemsSource = annees.Select(a => a.AnneeDebut).ToList();
        }
    }
}
