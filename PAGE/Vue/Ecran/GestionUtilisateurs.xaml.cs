using PAGE.Model;
using PAGE.Model.Enumerations;
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
    /// <author>Lucas</author>
    public partial class GestionUtilisateurs : Window, IObservateur
    {
        private Utilisateurs users;
        private UIElement initialContent;
        private List<Utilisateur> UserAffichage;
        private bool TriCroissant = false;
        private Promotion promo;

        /// <summary>
        /// Initialise la fenetre Gestion Utilisateurs
        /// </summary>
        /// <author>Lucas</author>

        public GestionUtilisateurs(Promotion promo)
        {
            InitializeComponent();
            this.promo = promo;
            initialContent = (UIElement?)this.Content;

            ChargementDiffereInitial();
        }

        /// <summary>
        /// Ouvre une fenêtre pour créer un utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void OpenCreerUtilisateur(object sender, RoutedEventArgs e)
        {
            if (users != null)
            {
                CreationUtilisateur creerUtilisateur = new CreationUtilisateur(new Utilisateur("", ""), users, promo);
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

        /// <summary>
        /// Chargement des utilisateurs différé via l'API
        /// </summary>
        /// <returns></returns>
        /// <author>Lucas</author>
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

        /// <summary>
        /// Chargement des utilisateurs différé via l'API et initialise la liste des utilisateurs à afficher
        /// </summary>
        /// <returns></returns>
        /// <author>Lucas</author>
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

        /// <summary>
        /// Une modification a ete recu, on raffraichis l'affichage
        /// </summary>
        /// <param name="Message"></param>
        /// <author>Lucas</author>
        public async void Notifier(string Message)
        {
            await Task.Delay(1000);

            ChargementDiffere();
        }
        /// <summary>
        /// Affiche les UserComponent pour les User de la liste
        /// </summary>
        /// <param name="listUsers"></param>
        /// <param name="typetri"></param>
        /// <author>Lucas</author>
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


        /// <summary>
        /// Double click sur un user, ouvre une page informative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void UserComponent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is UserComponent UserComponent)
            {
                // On recupère l'utilisateur associé au UserComponent
                Utilisateur userSelectionne = UserComponent.Utilisateur;

                if (userSelectionne != null)
                {
                   //non implémenté
                }
            }
        }

        /// <summary>
        /// tri les user par le login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void OrderByLogin(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les utilisateurs dans le bonne ordres
            AfficherLesUserComponent(UserAffichage, TYPETRI.APOGEE);
        }

        /// <summary>
        /// Quand on change le filtre selectionné dans la combobox des filtres ( pour l'instant il n'y en a qu'un)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
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

        /// <summary>
        /// Quand le texte du filtre/Recherche a changer on mets a jour l'affichage des users avec ce nouveau filtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
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

        /// <summary>
        /// Affiche la liste des Users filtré
        /// </summary>
        /// <param name="listEtudiants">liste des users a filtrer</param>
        /// <param name="filterType">type de filtre</param>
        /// <param name="filterText">texte saisi pour filtrer</param>
        /// <author>Lucas</author>
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


        /// <summary>
        /// Renvoi le filtre d'utilisateur 
        /// </summary>
        /// <param name="filterType">type de filtre choisi (login)</param>
        /// <param name="filterText">Texte saisi pour filtre</param>
        /// <returns></returns>
        /// <author>Lucas</author>
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
