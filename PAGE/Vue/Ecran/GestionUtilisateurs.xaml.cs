using PAGE.Model;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour GestionUtilisateurs.xaml
    /// </summary>
    public partial class GestionUtilisateurs : Window, IObservateur
    {
        private Utilisateurs users;
        private UIElement initialContent;
        private List<Utilisateur> UserAffichage;
        private bool TriCroissant = false;

        public GestionUtilisateurs()
        {
            InitializeComponent();

            initialContent = (UIElement?)this.Content;

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
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            
            this.users = new Utilisateurs(listUser);

            AfficherLesUserComponent(users.ListUser, TYPETRI.LOGIN);

            users.Register(this);
        }
        private async Task ChargementDiffereInitial()
        {
            //On récupère l'ensemble des utilisateurs via l'API
            List<Utilisateur> listUser;
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            
            this.users = new Utilisateurs(listUser);

            AfficherLesUserComponent(users.ListUser, TYPETRI.LOGIN);

            users.Register(this);

            UserAffichage = users.ListUser;
        }

        public async void Notifier(string Message)
        {
            await Task.Delay(1000);

            ChargementDiffere();
        }
        private void AfficherLesUserComponent(List<Utilisateur> listUsers, TYPETRI? typetri)
        {
            // On réinitialise le StackPanel
            StackPanelUser.Children.Clear();

            switch (typetri)
            {
                case TYPETRI.LOGIN:
                    listUsers = TriCroissant ?
                        listUsers.OrderByDescending(utilisateur => utilisateur.Login).ToList() :
                        listUsers.OrderBy(utilisateur => utilisateur.Login).ToList();
                        break;
                default:
                    break;
            }

            foreach (Utilisateur user in listUsers)
            {
                // Si l'utilisateur n'est pas déjà dans le StackPanel, on l'y ajoute
                if (!StackPanelUser.Children.OfType<UserComponent>().Any(uc => uc.Login == user.Login))
                {
                    // Ajoute le UserComponent personnalisé au StackPanel
                    UserComponent UserComponent = new UserComponent(user);
                    StackPanelUser.Children.Add(UserComponent);
                }
            }
        }



        private void UserComponent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is UserComponent UserComponent)
            {
                // On recupère l'utilisateur associé au UserComponent
                Utilisateur userSelectionne = UserComponent.Utilisateur;

                if (userSelectionne != null)
                {
                    // on affiche ces informations
                    //InformationsSupplementaires informationsSupplementaires = new InformationsSupplementaires(UserSelectionne, user);
                    //informationsSupplementaires.Show();
                }
            }
        }
        private void OrderByLogin(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les utilisateurs dans le bonne ordres
            AfficherLesUserComponent(UserAffichage, TYPETRI.APOGEE);
        }

        private void SelectionFiltreChanged(object sender, SelectionChangedEventArgs e)
        {
            //on recupere le filtre selectionner dans la combobox
            TYPETRI filterType = TYPETRI.LOGIN;
            switch (ComboBoxFiltre.SelectedIndex)
            {
                case 0:
                    filterType = TYPETRI.LOGIN;
                    break;
            }
            //on recupere le string saisi dans le textbox
            string filterText = TexteFiltre.Text;

            // Appel de la méthode avec le filtre sélectionné
            AfficherLesUserComponentFiltre(users.ListUser, filterType, filterText);
        }

        private void TexteFiltreChanged(object sender, TextChangedEventArgs e)
        {
            //si un filtre du combobox a été selectionner
            if (ComboBoxFiltre.SelectedIndex != -1)
            {
                //on recupere le filtre selectionner dans la combobox
                TYPETRI filterType = TYPETRI.LOGIN;
                switch (ComboBoxFiltre.SelectedIndex)
                {
                    case 0:
                        filterType = TYPETRI.LOGIN;
                        break;
                }
                //on recupere le string saisi dans le textbox
                string filterText = TexteFiltre.Text;

                // Appel de la méthode avec le filtre sélectionné
                AfficherLesUserComponentFiltre(UserAffichage, filterType, filterText);
            }
        }

        private void AfficherLesUserComponentFiltre(List<Utilisateur> listUsers, TYPETRI filterType, string filterText)
        {
            // On réinitialise le StackPanel
            StackPanelUser.Children.Clear();

            // Applique le filtre sur la liste d'utilisateurs
            List<Utilisateur> filteredList = (List<Utilisateur>)listUsers.Where(GetFilter(filterType, filterText)).ToList();

            if (String.IsNullOrEmpty(filterText))
            {
                ChargementDiffereInitial();
            }
            else
                UserAffichage = filteredList;


            AfficherLesUserComponent(filteredList, null);
        }

        private Func<Utilisateur, bool>? GetFilter(TYPETRI filterType, string filterText)
        {
            Func<Utilisateur, bool> filter = null;
            switch (filterType)
            {
                case TYPETRI.LOGIN:
                    filter = user => user.Login.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase);
                    break;
            }
            return filter;
        }
    }
}
