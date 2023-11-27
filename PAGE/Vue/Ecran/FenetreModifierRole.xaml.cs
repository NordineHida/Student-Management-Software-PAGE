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
        private List<Annee> annees = new List<Annee>();
        private Promotion promo;

        /// <summary>
        /// Fenetre de modification/Création de rôle
        /// </summary>
        /// <param name="user">utilisateur en question</param>
        /// <param name="annee">annee du role (-1 si on créer un role)</param>
        /// <param name="role">role /param>
        /// <author>Nordine</author>
        public FenetreModifierRole(Utilisateur user, int annee, ROLE role, Promotion promo)
        {
            InitializeComponent();
            DataContext= this;

            this.annee = annee;
            this.role = role;
            this.user = user;
            this.promo = promo;
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

                InitialisationComboBoxRole(role);
            }

        }



        //Quand on clique sur valider, mets a jour le rôle, ou le créer s'il n'existe pas
        private async void clickBtnValider(object sender, RoutedEventArgs e)
        {
            UtilisateurDAO userDAO= new UtilisateurDAO();
            await userDAO.UpdateRole(user, role, annee);
            GestionUtilisateurs gestionUtilisateurs = new GestionUtilisateurs(promo);
            gestionUtilisateurs.Show();

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

            ComboBoxAnnee.ItemsSource = annees;
            ComboBoxAnnee.DisplayMemberPath = "AnneeDebut";

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
                Annee anneeComboBox = (Annee)ComboBoxAnnee.SelectedItem;
                this.annee = anneeComboBox.AnneeDebut;

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


        /// <summary>
        /// Change la selection du combobox en fonction du role donnée
        /// </summary>
        /// <param name="role">role a mettre dans la combobox</param>
        /// <author>Nordine</author>
        private void InitialisationComboBoxRole(ROLE role)
        {
            int index;
            switch (role)
            {
                case ROLE.LAMBDA:
                    index = 0;
                    break;
                case ROLE.DIRECTEURDEPARTEMENT:
                    index = 1;
                    break;
                case ROLE.DIRECTEURETUDES1:
                    index = 2;
                    break;
                case ROLE.DIRECTEURETUDES2:
                    index = 3;
                    break;
                case ROLE.DIRECTEURETUDES3:
                    index = 4;
                    break;
                case ROLE.ADMIN:
                    index = 5;
                    break;
                default :
                    index = 0;
                    break;

            }

            ComboBoxRole.SelectedIndex = index;
        }

    }
}
