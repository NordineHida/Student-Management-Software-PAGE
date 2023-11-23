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
        private Annee annee;
        private Promotion promotion;

        public ChoixPromo()
        {
            InitializeComponent();

            ComboBoxAnnee.SelectionChanged += ComboBoxAnnee_SelectionChanged;

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

        private void ComboBoxAnnee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxAnnee.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content.ToString() == "Ajouter une nouvelle année")
            {
                
                
            }
        }
    }
}
