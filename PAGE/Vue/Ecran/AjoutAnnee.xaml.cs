using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.Windows;
using System.Windows.Input;

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
            get { return getAnneeSaisie(); }
        }
        public AjoutAnnee()
        {
            InitializeComponent();
            txtAnneeSaisie.PreviewTextInput += TextBox_PreviewTextInput;
        }

        /// <summary>
        /// Verifie qu'on saisi seulement des chiffres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // On verifie si c'est un nombre
            if (!Char.IsDigit(e.Text, 0))
            {
                // Ignore le caractère si ce n'est pas un chiffre
                e.Handled = true; 
            }
        }

        /// <summary>
        /// renvoi l'année saisi si elle est correct (entre 1800 et mtn+ans)
        /// </summary>
        /// <returns>année saisi</returns>
        /// <author>Nordine</author>
        private int getAnneeSaisie()
        {
            int anneeSaisie=-1;

            if (Int32.TryParse(txtAnneeSaisie.Text, out anneeSaisie))
            {
                int anneeMin = 1800;
                int anneeMax = DateTime.Now.Year + 2;

                if (anneeSaisie < anneeMin || anneeSaisie > anneeMax)
                {
                    anneeSaisie = -1;

                    // Afficher la pop-up d'erreur
                    string titre = (Parametre.Instance.Langue == LANGUE.FRANCAIS) ? "Erreur année" : "Bad year";
                    string message = (Parametre.Instance.Langue == LANGUE.FRANCAIS) ? "Veuillez sélectionner une année correcte" : "Please select a correct year";

                    PopUp popUp = new PopUp(titre, message, TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                    anneeSaisie = Int32.Parse(txtAnneeSaisie.Text);
            }

            return -1; 
        }


        /// <summary>
        /// Ferme la fenêtre lors du click sur le bouton OK
        /// </summary>
        /// <author>Yamato</author>
        private void OkClick(object sender, RoutedEventArgs e)
        {

    

                this.Close();
           
            else
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Erreur année", "Veuillez selectionner une année correcte", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Bad year", "Please select a correct year", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
