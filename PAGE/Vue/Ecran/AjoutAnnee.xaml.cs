using PAGE.Model;
using PAGE.Model.PatternObserveur;
using System;
using System.Windows;

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
        /// <author>Nordine/Yamato</author>
        public int AnneeSaisie
        {
            get { return Int32.Parse(txtAnneeSaisie.Text); }
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
            if (AnneeSaisie != null)
            {

                this.Close();
            }
            else
            {
                PopUp popUp = new PopUp("Erreur année", "Veuillez selectionner une année", TYPEICON.ERREUR);
                popUp.ShowDialog();
            }
        }

    }
}
