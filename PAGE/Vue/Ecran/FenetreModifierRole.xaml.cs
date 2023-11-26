using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetreModifierRole.xaml
    /// </summary>
    public partial class FenetreModifierRole : Window
    {
        private int annee;
        private ROLE role;
        private Utilisateur user;
        private bool modeCreation;
        private List<Annee> annees;

        public FenetreModifierRole(Utilisateur user, int annee, ROLE role)
        {
            InitializeComponent();

            this.annee = annee;
            this.role = role;
            this.user = user;

            //si année incorrect alors on est en mode création, sinon mode affichage/modification de rôle
            modeCreation = annee < 0;
            
            //Si on est en mode création de rôle
            if (modeCreation)
            {
                //on active la comboBox et recupere les années possible
                ComboBoxAnnee.IsEnabled = true;
                MettreAJourComboBox();

                //par defaut le rôle est "pas de rôle"
                ComboBoxRole.SelectedIndex= 0;
            }
            //Mode affichage/modification de rôle seulement
            else
            {
                ComboBoxAnnee.SelectedItem = annee;
                ComboBoxAnnee.IsEnabled = false;
            }

        }


        //Quand on clique sur valider, mets a jour le rôle, ou le créer s'il n'existe pas
        private void clickBtnValider(object sender, RoutedEventArgs e)
        {
            UtilisateurDAO userDAO= new UtilisateurDAO();
            //userDAO.UpdateRole(user, role, annee);

            this.Close();
        }

        /// <summary>
        /// ferme la fenetre sans sauvegarder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void clickBtnAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Mets à jour la combobox en triant de la plus petite année à la plus grande
        /// </summary>
        /// <author>Yamato</author>
        private async void MettreAJourComboBox()
        {
            AnneeDAO dao = new AnneeDAO();
            annees = await dao.GetAllAnnee();

            // Tri de la liste des années par ordre croissant
            List<Annee> anneesTriees = annees.OrderBy(a => a.AnneeDebut).ToList();

            ComboBoxAnnee.ItemsSource = anneesTriees;
        }

        /// <summary>
        /// change l'attribut année quand on change la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxAnnee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboBoxAnnee.SelectedItem != null)
            {
                // Convertissez l'objet sélectionné en int 
                if (int.TryParse(ComboBoxAnnee.SelectedItem.ToString(), out int selectedYear))
                {
                    //on l'affecte a l'attribut
                    annee = selectedYear;
                }
            }
        }


        /// <summary>
        /// change l'attribut Role quand on change la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxRole_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (ComboBoxRole.SelectedIndex)
            {
                case 0:
                    this.role = ROLE.LAMBDA;
                    break;
                case 1:
                    this.role = ROLE.DIRECTEURDEPARTEMENT;
                    break;
                case 2:
                    this.role = ROLE.DIRECTEURETUDES1;
                    break;
                case 3:
                    this.role = ROLE.DIRECTEURETUDES2;
                    break;
                case 4:
                    this.role = ROLE.DIRECTEURETUDES3;
                    break;
                case 5:
                    this.role = ROLE.ADMIN;
                    break;
                default:
                    this.role = ROLE.LAMBDA;
                    break;
            }
        }
    }
}
