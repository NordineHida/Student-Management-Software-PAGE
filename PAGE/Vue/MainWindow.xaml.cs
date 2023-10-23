using PAGE.Vue.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Windows.Navigation;
using Microsoft.Win32;
using PAGE.APIEtudiant.Stockage;
using PAGE.Model;

namespace PAGE.Vue
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private UIElement initialContent;
        public MainWindow()
        {
            InitializeComponent();

            initialContent = (UIElement?)this.Content;

            ChargerListView();
            ChargementDiffere();

        }

        private void ChargerListView()
        {
            
            GridView gridView = new GridView();
            maListView.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "N°Apogee",
                DisplayMemberBinding = new System.Windows.Data.Binding("NumApogee")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Nom",
                DisplayMemberBinding = new System.Windows.Data.Binding("Nom")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Prenom",
                DisplayMemberBinding = new System.Windows.Data.Binding("Prenom")
            });


        } 

        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        private async Task ChargementDiffere()
        {
            //On reinitialise la liste
            maListView.Items.Clear();

            //On récupere l'ensemble des étudiants via l'API
            List<Etudiant> etudiants = (await EtuDAO.Instance.GetAllEtu()).ToList();

            foreach (Etudiant etu in etudiants)
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if(!maListView.Items.Contains(etu))
                    maListView.Items.Add(etu);
            }
        }

        private void OpenLoginPage(object sender, RoutedEventArgs e)
        {
            // Créer la LoginPage
            LoginPage loginPage = new LoginPage();

            // Écouter l'événement de retour à la fenêtre principale
            loginPage.ReturnToMainWindow += LoginPage_ReturnToMainWindow;

            // Afficher la LoginPage comme contenu initial
            this.Content = loginPage;

        }

        private void LoginPage_ReturnToMainWindow(object sender, EventArgs e)
        {
            this.Content = initialContent;
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
        private void BoutonActualiserListeEtudiant(object sender, RoutedEventArgs e)
        {
            ActualiserEtudiant();

            //On ouvre une pop-up pour indiquer qu'on a bien actualiser
            MessageBox.Show("Vous avez bien actualisé", "Actualisation avec succès", MessageBoxButton.OK);
        }

        /// <summary>
        /// Actualise l'affichage de la liste des étudiants
        /// </summary>
        private async void ActualiserEtudiant()
        {
            await ChargementDiffere(); 
            ChargerListView();
        }
    }
}
