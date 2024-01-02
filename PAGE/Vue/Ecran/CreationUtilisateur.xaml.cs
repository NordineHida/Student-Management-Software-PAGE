
using PAGE.Model;
using PAGE.Model.Enumerations;
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
        Promotion promo;
        public CreationUtilisateur(Utilisateur user,Utilisateurs users, Promotion promo)
        {
            this.user = user;
            this.users = users;
            DataContext = user;
            InitializeComponent();
            this.promo = promo;
        }

        private void ClickCreer(object sender, RoutedEventArgs e)
        {
            if (isCreateOk()) 
            {
                IUtilisateurDAO dao = new UtilisateurDAO();
                dao.CreateUtilisateur(user,promo.AnneeDebut);
                users.AddUser(user);
                this.Close();
            }
            
        }

        private void ClickAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool isCreateOk()
        {
            bool ok = true;
            if (txtLogin.Text == "")
            {
                ok = false;
                
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ de login ne doit pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The login field must not be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
            if (txtMDP.Password == "")
            {
                ok = false;
                
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Le champ de mot de passe ne doit pas être vide", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "The password field must not be empty", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
            else foreach (Utilisateur user in users.ListUser)
                {
                    if (user.Login == txtLogin.Text)
                    {
                        ok = false;

                        if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                        {
                            PopUp popUp = new PopUp("Création", "Ce login est déjà pris : Choisissez-en un autre", TYPEICON.ERREUR);
                            popUp.ShowDialog();
                        }
                        else
                        {
                            PopUp popUp = new PopUp("Creation", "This login is already taken: Choose another one", TYPEICON.ERREUR);
                            popUp.ShowDialog();
                        }
                    }
                       
                }
            return ok;
        }
    }
}

