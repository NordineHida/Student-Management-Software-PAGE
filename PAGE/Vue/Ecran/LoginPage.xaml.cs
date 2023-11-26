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
        private Promotion promo;
        private Token token;
        public LoginPage(Utilisateur user,Promotion promo, Token? tokenUtilisateur)
        {
            this.user = user;
            this.promo = promo;
            if (tokenUtilisateur != null)
            {
                this.token = tokenUtilisateur;
            }
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
            FenetrePrincipal fenetrePrincipal;
            if (token == null)
            {
                fenetrePrincipal = new FenetrePrincipal(promo, null);
            }
            else
            {
                fenetrePrincipal = new FenetrePrincipal(promo, token);
            }
            fenetrePrincipal.Show();

            this.Close();

        }


        /// <summary>
        /// Ferme la page de login et affiche la fenetre principal avec les nouvelles informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo</author>
        private async void BoutonLogin(object sender, RoutedEventArgs e)
        {
            user.Mdp = txtPassword.Password;

            IUtilisateurDAO dao = new UtilisateurDAO();
            token = await dao.Connexion(user.Login,user.HashMdp);
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal(promo,token);
            fenetrePrincipal.Show();

            this.Close();
        }
    }
}
