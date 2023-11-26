using PAGE.Model;
using PAGE.Model.Enumerations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour Parametre.xaml
    /// </summary>
    public partial class ParametrePage : Window
    {
        //on sauve la promo et le token pour la redonner à la fenetre principal quand on ferme les parametres
        private Promotion promo;
        private Token token;

        /// <summary>
        /// Constructeur de paramètre (initialise le path du word au bureau)
        /// </summary>
        /// <param name="promo">promotion actuelle</param>
        /// <author>Nordine</author>
        public ParametrePage(Promotion promo, Token? tokenUtilisateur)
        {
            InitializeComponent();
            this.promo = promo;
            if (tokenUtilisateur != null)
            {
                this.token = tokenUtilisateur;
            }
            InitialiserComboBoxSelectedItem();

            //Initialise le label
            LabelPathGeneration.Text = Parametre.Instance.PathGenerationWord;
            this.token = token;
        }

        /// <summary>
        /// Initialise la ComboBox en fonction de la langue des paramètres
        /// </summary>
        /// <param name="codeLangue">code de la langue des paramètres</param>
        /// <author>Nordine</author>
        private void InitialiserComboBoxSelectedItem()
        {
            switch (Parametre.Instance.Langue)
            {
                case LANGUE.ANGLAIS:
                    ComboBoxLangue.SelectedIndex = 1; // English
                    break;
                case LANGUE.FRANCAIS:
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
            FermerFenetre();

        }

        /// <summary>
        /// Ferme la fenetre actuelle et ouvre la fenetre principal
        /// </summary>
        /// <author>Nordine</author>
        private void FermerFenetre()
        {
            FenetrePrincipal fenetrePrincipal;
            if (this.token != null)
            {
                fenetrePrincipal = new FenetrePrincipal(promo,token);
            }
            else
            {
                fenetrePrincipal = new FenetrePrincipal(promo, null);
            }
            
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
            //Si un item est choisi
            if (ComboBoxLangue.SelectedItem != null)
            {
                //on récupere quel langue est selectionnée
                ComboBoxItem langueChoisie = (ComboBoxItem)ComboBoxLangue.SelectedItem;
                string langueChoisieString = langueChoisie.Content.ToString();

                ChangerLangue(langueChoisieString);
            }

            //on sauvegarde les paramètres
            JsonSerializerParametre jsonSave = new JsonSerializerParametre();
            jsonSave.Save();

            FermerFenetre();

        }

        /// <summary>
        /// Change le chemin de génération du word (par défaut va sur le bureau)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ChangerPathWord(object sender, RoutedEventArgs e)
        {

            //Ouvrir l'explorateur de fichier au dernier répertoire sauvé (ou bureau par défaut)
            string precedentChemin = Parametre.Instance.PathGenerationWord;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = precedentChemin;
            DialogResult resultat = dialog.ShowDialog();

            //Si on valide, on récupere le nouveau path du word
            if (resultat == System.Windows.Forms.DialogResult.OK )
            {
                Parametre.Instance.PathGenerationWord = dialog.SelectedPath;
            }

            //On actualise le texte du label
            LabelPathGeneration.Text = Parametre.Instance.PathGenerationWord;

        }


        /// <summary>
        /// Change le dictionnaire de ressources utilisé pour changer la langue
        /// </summary>
        /// <param name="langueCode">code de langue à utiliser</param>
        /// <author>Nordine</author>
        private void ChangerLangue(string langueCode)
        {
            switch(langueCode)
            {
                case "English":
                    Parametre.Instance.Langue = LANGUE.ANGLAIS;
                    break;
                case "Français":
                    Parametre.Instance.Langue = LANGUE.FRANCAIS;
                    break;
                default:
                    Parametre.Instance.Langue = LANGUE.FRANCAIS;
                    break;
            }
        }

    }
}
