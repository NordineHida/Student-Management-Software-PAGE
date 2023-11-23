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
            this.Close();
        }
    }
}
