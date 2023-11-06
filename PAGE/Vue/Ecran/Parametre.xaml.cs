using System;
using System.Windows;
using System.Windows.Forms;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour Parametre.xaml
    /// </summary>
    public partial class Parametre : Window
    {
        private string pathGenerationWord;
        

        /// <summary>
        /// Chemin de génération du word final
        /// </summary>
        /// <author>Nordine</author>
        public string PathGenerationWord { get=> pathGenerationWord;}

        /// <summary>
        /// Constructeur de paramètre (initialise le path du word au bureau)
        /// </summary>
        public Parametre()
        {
            InitializeComponent();
            //Initialise le chemin de generation au bureau (DEPUIS SAUVEGARDE JSON APRES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!)
            this.pathGenerationWord = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //Initialise le label
            LabelPathGeneration.Text = pathGenerationWord;
        }

        /// <summary>
        /// Ferme la page de parametre et affiche la fenetre principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal();
            fenetrePrincipal.Show();

            this.Close();

        }

        /// <summary>
        /// Valide les paramètres effectués
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ValiderParam(object sender, RoutedEventArgs e)
        {
            //LEs N'EST PAS ENCORE IMPLEMENTé DOIT SAUVEGARDER DANS UN JSON
            throw new NotImplementedException();
        }


        /// <summary>
        /// Change le chemin de génération du word (par défaut va sur le bureau)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangerPathWord(object sender, RoutedEventArgs e)
        {
            //Ouvrir l'explorateur de fichier au bureau
            string cheminBureau = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = cheminBureau;
            DialogResult resultat = dialog.ShowDialog();

            //Si on valide, on récupere le nouveau path du word
            if (resultat == System.Windows.Forms.DialogResult.OK )
            {
                pathGenerationWord= dialog.SelectedPath;
            }

            //On actualise le texte du label
            LabelPathGeneration.Text = pathGenerationWord;

        }

    }
}
