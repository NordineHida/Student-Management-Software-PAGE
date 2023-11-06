using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour Parametre.xaml
    /// </summary>
    public partial class Parametre : Window
    {
        private string pathGenerationWord;

        private string langueCode;

        /// <summary>
        /// Chemin de génération du word final
        /// </summary>
        /// <author>Nordine</author>
        public string PathGenerationWord { get=> pathGenerationWord;}

        /// <summary>
        /// Constructeur de paramètre (initialise le path du word au bureau)
        /// </summary>
        /// <author>Nordine</author>
        public Parametre()
        {
            InitializeComponent();
            //Initialise le chemin de generation au bureau (DEPUIS SAUVEGARDE JSON APRES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!)
            this.pathGenerationWord = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //Initialise la langue  (DEPUIS SAUVEGARDE JSON APRES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!)
            this.langueCode = "fr";
            InitialiserComboBoxSelectedItem();


            //Initialise le label
            LabelPathGeneration.Text = pathGenerationWord;
        }

        /// <summary>
        /// Initialise la ComboBox en fonction de la langueCode
        /// </summary>
        /// <param name="codeLangue">code de la langue des paramètres</param>
        /// <author>Nordine</author>
        private void InitialiserComboBoxSelectedItem()
        {
            switch (this.langueCode)
            {
                case "en":
                    ComboBoxLangue.SelectedIndex = 1; // English
                    break;
                case "fr":
                    ComboBoxLangue.SelectedIndex = 0; // Français
                    break;
                default:
                    ComboBoxLangue.SelectedIndex = 0; // Français (par défaut)
                    break;
            }
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
        /// <author>Nordine</author>
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

        /// <summary>
        /// Change la langue séléctionner dans la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Si un item est choisi
            if (ComboBoxLangue.SelectedItem != null)
            {
                //on récupere quel langue est selectionnée
                ComboBoxItem langueChoisie = (ComboBoxItem)ComboBoxLangue.SelectedItem;
                string? langueChoisieString = langueChoisie.Content.ToString();

                //Switch des langues possibles avec français par défaut
                switch (langueChoisieString)
                {
                    case "English":
                        ChangerLangue("en");
                        break;
                    case "Français":
                        ChangerLangue("fr");
                        break;
                    default:
                        ChangerLangue("fr");
                        break;
                }

            }
        }

        /// <summary>
        /// Change le dictionnaire de ressources utilisé pour changer la langue
        /// </summary>
        /// <param name="langueCode">code de langue à utiliser</param>
        /// <author>Nordine</author>
        private void ChangerLangue(string langueCode)
        {
            ResourceDictionary dictionnaire = new ResourceDictionary();

            switch(langueCode)
            {
                case "en":
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "fr":
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
                default:
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
            }

            this.Resources.MergedDictionaries.Add(dictionnaire);

        }

    }
}
