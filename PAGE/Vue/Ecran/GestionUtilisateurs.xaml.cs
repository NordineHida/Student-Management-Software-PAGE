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
    /// Logique d'interaction pour GestionUtilisateurs.xaml
    /// </summary>
    public partial class GestionUtilisateurs : Window
    {
        private Utilisateurs users;

        public GestionUtilisateurs()
        {
            InitializeComponent();
            ChargementDiffereInitial();
        }

        private void OpenCreerUtilisateur(object sender, RoutedEventArgs e)
        {
            if (users != null)
            {
                CreationUtilisateur creerUtilisateur = new CreationUtilisateur(new Utilisateur("", ""), users);
                creerUtilisateur.Show();
            }
            else
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Erreur de chargement", "La liste d'utilisateurs n'a pas encore été chargé. Veuillez patienter..", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Loading error", "The user list has not yet been loaded. Please wait..", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
        }

        private async Task ChargementDiffere()
        {
            //On récupère l'ensemble des utilisateurs via l'API
            List<Utilisateur> listUser;
            Dictionary<string, Utilisateur> dicoUser = new Dictionary<string, Utilisateur>();
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            for (int i = 0; i < listUser.Count; i++)
            {
                dicoUser.Add(listUser[i].Login, listUser[i]);
            }
            this.users = new Utilisateurs(dicoUser);
        }
        private async Task ChargementDiffereInitial()
        {
            //On récupère l'ensemble des utilisateurs via l'API
            List<Utilisateur> listUser;
            Dictionary<string, Utilisateur> dicoUser = new Dictionary<string, Utilisateur>();
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            for (int i = 0; i < listUser.Count; i++)
            {
                dicoUser.Add(listUser[i].Login, listUser[i]);
            }
            this.users = new Utilisateurs(dicoUser);
        }
        private void OrderByLogin(object sender, RoutedEventArgs e)
        {

        }
    }
}
