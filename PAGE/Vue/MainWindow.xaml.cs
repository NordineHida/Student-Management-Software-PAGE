using PAGE.Vue.Ressources;
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
    }
}
