using DocumentFormat.OpenXml.Spreadsheet;
using PAGE.Model;
using PAGE.Stockage;
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
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private Utilisateur user;
        public LoginPage(Utilisateur user)
        {
            this.user = user;
            DataContext = user;
            InitializeComponent();
        }

        /// <summary>
        /// Ferme la page de login et affiche la fenetre principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void CloseLoginWindow(object sender, RoutedEventArgs e)
        {
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal();
            fenetrePrincipal.Show();

            this.Close();

        }


        /// <summary>
        /// Ferme la page de login et affiche la fenetre principal avec les nouvelles informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonLogin(object sender, RoutedEventArgs e)
        {
            user.Mdp = txtPassword.Password;

            IUtilisateurDAO dao = new UtilisateurDAO();
            Token token = await dao.Connexion(user.Login,user.HashMdp);
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal(token);
            fenetrePrincipal.Show();

            this.Close();
        }
    }
}
