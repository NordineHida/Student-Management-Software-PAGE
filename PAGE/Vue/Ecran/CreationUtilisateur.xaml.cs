
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
    /// Logique d'interaction pour CreationUtilisateur.xaml
    /// </summary>
    public partial class CreationUtilisateur : Window
    {
        Utilisateur user;
        Utilisateurs users;
        public CreationUtilisateur(Utilisateur user,Utilisateurs users)
        {
            this.user = user;
            this.users = users;
            DataContext = user;
            InitializeComponent();
        }

        private void ClickCreer(object sender, RoutedEventArgs e)
        {

            UtilisateurDAO dao = new UtilisateurDAO();
            dao.CreateUtilisateur(user);
            users.AddUser(user);
            this.Close();
        }

        private void ClickAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
