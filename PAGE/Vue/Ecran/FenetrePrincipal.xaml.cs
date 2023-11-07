using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using PAGE.APIEtudiant.Stockage;
using PAGE.Model;
using PAGE.Vue.Ressources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour FenetrePrincipal.xaml
    /// </summary>
    public partial class FenetrePrincipal : Window
    {
        private UIElement initialContent;

        /// <summary>
        /// Initialise la fenetre principal
        /// </summary>
        /// <author>Nordine & Stephane</author>
        public FenetrePrincipal()
        {
            InitializeComponent();

            initialContent = (UIElement?)this.Content;

            ChargementDiffere();

        }

        /// <summary>
        /// trie la liste a partir de la liste cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stéphane</author>
        private bool isSortAscending = true;

        private void Trie_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);

            string sortBy = column.Tag.ToString();

            // Vérifie s'il existe des descriptions de tri pour les éléments de la liste.
            if (maListView.Items.SortDescriptions.Count > 0)
            {
                // Efface les descriptions de tri existantes.
                maListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir;

            // Détermine la direction de tri en fonction de la valeur de 'isSortAscending'.
            // Si 'isSortAscending' est vrai, le tri est défini sur croissant, sinon sur décroissant.
            // Inverse ensuite la valeur de 'isSortAscending'.
            if (isSortAscending)
            {
                newDir = ListSortDirection.Ascending;
                isSortAscending = false;
            }
            else
            {
                newDir = ListSortDirection.Descending;
                isSortAscending = true;
            }

            // Ajouter une nouvelle description de tri à la liste 
            maListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }


        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        /// <author>Nordine & Stephane</author>
        private async Task ChargementDiffere()
        {
            //On reinitialise la liste
            maListView.Items.Clear();

            //On récupere l'ensemble des étudiants via l'API
            List<Etudiant> etudiants = (await EtuDAO.Instance.GetAllEtu()).ToList();

            foreach (Etudiant etu in etudiants)
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if (!maListView.Items.Contains(etu))
                    maListView.Items.Add(etu);
            }
        }


        /// <summary>
        /// Ouvre la fenetre de connexion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenLoginPage(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();

            this.Close();
        }

        private void OpenParametresPage(object sender, RoutedEventArgs e)
        {
            // Créer la LoginPage
            ParametresPage parampage = new ParametresPage();

            // Écouter l'événement de retour à la fenêtre principale
            parampage.ReturnToMainWindow += ParamPage_ReturnToMainWindow;

            // Afficher la LoginPage comme contenu initial
            this.Content = parampage;
        }

        private void ParamPage_ReturnToMainWindow(object sender, EventArgs e)
        {
            this.Content = initialContent;
        }

        /// <summary>
        /// Quand on clique sur le bouton importer pour chercher le fichier excel avec les étudiants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine & Stephane</author>
        private void ImporterEtudiants(object sender, RoutedEventArgs e)
        {
            // Utilisez OpenFileDialog pour permettre à l'utilisateur de sélectionner un fichier
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtenez le chemin du fichier sélectionné
                string selectedFilePath = openFileDialog.FileName;

                // Appelez la méthode GetEtudiants avec le chemin du fichier
                LecteurExcel lc = new LecteurExcel();
                EtuDAO.Instance.AddSeveralEtu(lc.GetEtudiants(selectedFilePath));
            }

            //On actualise l'affichage
            ActualiserEtudiant();
        }

        /// <summary>
        /// Actualise la liste des étudiants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void BoutonActualiserListeEtudiant(object sender, RoutedEventArgs e)
        {
            ActualiserEtudiant();

            //On ouvre une pop-up pour indiquer qu'on a bien actualiser
            MessageBox.Show("Vous avez bien actualisé", "Actualisation avec succès", MessageBoxButton.OK);
        }

        /// <summary>
        /// Actualise l'affichage de la liste des étudiants
        /// </summary>
        /// <author>Nordine</author>
        private async void ActualiserEtudiant()
        {
            await ChargementDiffere();
        }

        /// <summary>
        /// Ouvre la fenetre de choix de l'année de et promotion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPromoPage(object sender, RoutedEventArgs e)
        {
            ChoixPromo choixPromo = new ChoixPromo();
            choixPromo.Show();
            this.Close();
        }

    }
}

