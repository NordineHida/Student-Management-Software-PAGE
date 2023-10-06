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

            GridView gridView = new GridView();
            maListView.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "numApogee",
                DisplayMemberBinding = new System.Windows.Data.Binding("numApogee")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "nom",
                DisplayMemberBinding = new System.Windows.Data.Binding("nom")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "prenom",
                DisplayMemberBinding = new System.Windows.Data.Binding("prenom")
            });

            maListView.Items.Add(new DataObject { numApogee = 1234, nom = "basset", prenom = "stephane" });
            maListView.Items.Add(new DataObject { numApogee = 5678, nom = "hida", prenom = "nordine" });
            maListView.Items.Add(new DataObject { numApogee = 9801, nom = "duszynski", prenom = "laszlo" });
        
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
        private void Button_Click(object sender, RoutedEventArgs e)
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
                APIEtuDAO.Instance.AddSeveralEtu(lc.GetEtudiants(selectedFilePath));
            }
        }
    }

    #region pour affiche liste etudiant
    public class DataObject
    {
        public int numApogee { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
    }

    #endregion
}
