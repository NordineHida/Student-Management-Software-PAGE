using PAGE.Model;
using PAGE.Model.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetreRolesUser.xaml
    /// </summary>
    public partial class FenetreRolesUser : Window
    {
        private bool TriCroissant = false;
        private Utilisateur user;
        private Promotion promo;

        public FenetreRolesUser(Utilisateur user, Promotion promo)
        {
            InitializeComponent();
            AfficherLesEtuComponent(user.Roles, TYPETRI.ANNEE);
            this.user = user;
            this.promo = promo;

        }

        private void RoleEtAnneeComponent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is RoleEtAnneeComponent roleComponent)
            {
                // On recupère l'année et le role sélectionner
                int anneeSelectionne = roleComponent.Annee;
                ROLE roleSelectionne = roleComponent.Role;

                // on affiche ces informations
                FenetreModifierRole FmodifRole = new FenetreModifierRole(this.user,anneeSelectionne, roleSelectionne,promo); ;
                FmodifRole.Show();
                this.Close();

            }
        }

        /// <summary>
        /// Affiche les EtudiantComponent pour les Etudiant de la liste
        /// </summary>
        /// <param name="listEtudiants">liste des etudiants à afficher</param>
        /// <param name="typetri">type de tri</param>
        /// <author>Nordine</author>
        private void AfficherLesEtuComponent(Dictionary<int,ROLE> listRole, TYPETRI? typetri)
        {
            // On réinitialise le StackPanel
            StackPanelRoles.Children.Clear();

            switch (typetri)
            {
                //tri par année dans l'odre (De)croissant
                case TYPETRI.ANNEE:
                    listRole = TriCroissant
                        ? listRole.OrderBy(kvp => kvp.Key).ThenByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                        : listRole.OrderByDescending(kvp => kvp.Key).ThenByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;

                //tri par role
                case TYPETRI.ROLE:
                    listRole = TriCroissant
                        ? listRole.OrderBy(kvp => kvp.Value).ThenBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                        : listRole.OrderByDescending(kvp => kvp.Value).ThenByDescending(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;

            }

            foreach (int annee in listRole.Keys)
            {
                // Si l'année n'est pas déjà dans le StackPanel, on l'y ajoute
                if (!StackPanelRoles.Children.OfType<RoleEtAnneeComponent>().Any(uc => uc.Annee == annee))
                {
                    // Ajoute le rôle personnalisé au StackPanel
                    RoleEtAnneeComponent roleComponent = new RoleEtAnneeComponent(annee, listRole[annee]);
                    StackPanelRoles.Children.Add(roleComponent);
                }
            }
        }

        /// <summary>
        /// ouvre la fenetre de modifcation role en mode Création (année <0)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void AjouterRole_Click(object sender, RoutedEventArgs e)
        {
            FenetreModifierRole fmr = new FenetreModifierRole(user, -1, ROLE.LAMBDA,this.promo);
            fmr.Show();
            this.Close();
        }
        
    }
}
