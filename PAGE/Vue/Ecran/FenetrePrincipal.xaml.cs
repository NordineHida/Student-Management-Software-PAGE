using PAGE.Model.PatternObserveur;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Linq;
using PAGE.Model.Enumerations;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetrePrincipal.xaml
    /// </summary>
    public partial class FenetrePrincipal : Window, IObservateur
    {
        private Utilisateurs users;
        private Etudiants etudiants;
        private List<Etudiant> etudiantAffichage;
        private bool TriCroissant=false;

        Promotion promo;

        /// <summary>
        /// Initialise la fenetre principal
        /// </summary>
        /// <author>Nordine & Stephane</author>
        public FenetrePrincipal()
        {
            InitializeComponent();
            ChargementDiffereInitial();

        }

        /// <summary>
        /// Initialise la fenetre principal depuis l'annee selectionner
        /// </summary>
        /// <param name="promo">Promo d'étudiant selectionner</param>
        /// <author>Nordine/Yamato</author>
        public FenetrePrincipal(Promotion promo)
        {
            InitializeComponent();
            this.promo = promo;
            ChargementDiffereInitial();
            

        }


        /// <summary>
        /// Chargement des etudiants différé via l'API et initisalise la liste d'étudiants à afficher
        /// </summary>
        /// <author>Nordine</author>
        private async Task ChargementDiffereInitial()
        {
            // On récupère l'ensemble des étudiants via l'API
            EtuDAO etuDAO = new EtuDAO();
            this.etudiants = new Etudiants((List<Etudiant>)await etuDAO.GetAllEtu(this.promo));

            //On récupère l'ensemble des utilisateurs via l'API
            List<Utilisateur> listUser;
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            
            this.users = new Utilisateurs(listUser);


            //Affiche les components des etudiants (trie par numero apogee par defaut
            AfficherLesEtuComponent(etudiants.ListeEtu, TYPETRI.APOGEE);

            // On enregistre cette fenetre comme observeur des notes
            etudiants.Register(this);

            //initialisation etudiant a afficher
            etudiantAffichage = etudiants.ListeEtu;
        }

        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        /// <author>Nordine & Stephane</author>
        private async Task ChargementDiffere()
        {
            // On récupère l'ensemble des étudiants via l'API
            EtuDAO dao = new EtuDAO();
            this.etudiants = new Etudiants((List<Etudiant>)await dao.GetAllEtu(promo));

            //On récupère l'ensemble des utilisateurs via l'API
            List<Utilisateur> listUser;
            UtilisateurDAO userDAO = new UtilisateurDAO();
            listUser = (List<Utilisateur>)await userDAO.GetAllUtilisateurs();
            
            this.users = new Utilisateurs(listUser);

            //Affiche les components des etudiants (trie par numero apogee par defaut
            AfficherLesEtuComponent(etudiants.ListeEtu,TYPETRI.APOGEE);

            // On enregistre cette fenetre comme observeur des notes
            etudiants.Register(this);
        }



        /// <summary>
        /// Ouvre la fenetre de connexion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OpenLoginPage(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(new Utilisateur("",""));
            loginPage.Show();

            this.Close();
        }

        /// <summary>
        /// Ouvre la fenêtre des paramètres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OpenParametresPage(object sender, RoutedEventArgs e)
        {
            ParametrePage parametre = new ParametrePage();
            parametre.Show();

            this.Close();
        }


        /// <summary>
        /// Quand on clique sur le bouton importer pour chercher le fichier excel avec les étudiants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine & Stephane</author>
        private void ImporterEtudiants(object sender, RoutedEventArgs e)
        {
            // Utilisez OpenFileDialog pour permettre à l'utilisateur de sélectionner un fichier
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtenez le chemin du fichier sélectionné
                string selectedFilePath = openFileDialog.FileName;

                // Appelez la méthode GetEtudiants avec le chemin du fichier
                LecteurExcel lc = new LecteurExcel();
                EtuDAO dao = new EtuDAO();
                dao.AddSeveralEtu(lc.GetEtudiants(selectedFilePath),this.promo);
            }

            //On actualise l'affichage
            ActualiserEtudiant();
        }

        /// <summary>
        /// Actualise la liste des étudiants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void BoutonActualiserListeEtudiant(object sender, RoutedEventArgs e)
        {
            ActualiserEtudiant();
        }

        /// <summary>
        /// Actualise l'affichage de la liste des étudiants
        /// </summary>
        /// <author>Nordine</author>
        private async void ActualiserEtudiant()
        {
            await ChargementDiffere();
        }

        /// <summary>
        /// Ouvre la fenetre de choix de l'année de et promotion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OpenPromoPage(object sender, RoutedEventArgs e)
        {
            ChoixPromo choixPromo = new ChoixPromo();
            choixPromo.Show();
            this.Close();
        }


        /// <summary>
        /// Ouvre la fenêtre pour créer un étudiant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void BoutonCreerEtudiant(object sender, RoutedEventArgs e)
        {
            if (etudiants != null)
            {
                FenetreCreerEtudiant fenetreCreerEtudiant = new FenetreCreerEtudiant(etudiants,this.promo);
                fenetreCreerEtudiant.Show();
            }
            else
            {
                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création", "Veuillez attendre la fin du chargement des étudiants", TYPEICON.INFORMATION);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Creation", "Please wait until the students have finished loading", TYPEICON.INFORMATION);
                    popUp.ShowDialog();
                }
            }

        }

        /// <summary>
        /// Une modification a ete recu, on raffraichis l'affichage
        /// </summary>
        /// <param name="Message">message specifique</param>
        /// <author>Nordine/Laszlo</author>
        public async void Notifier(string Message)
        {
            await Task.Delay(1000);

            ChargementDiffere();
        }

        /// <summary>
        /// Ouvre la fenêtre Informations Supplémentaires lors d'un double clique sur un étudiant de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void EtudiantComponent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is EtudiantComponent EtudiantComponent)
            {
                // On recupère l'étudiant associé au EtudiantComponent
                Etudiant etudiantSelectionne = EtudiantComponent.Etudiant;

                if (etudiantSelectionne != null)
                {
                    // on affiche ces informations
                    InformationsSupplementaires informationsSupplementaires = new InformationsSupplementaires(etudiantSelectionne, etudiants,this.promo);
                    informationsSupplementaires.Show();
                }
            }
        }

        /// <summary>
        /// Affiche les EtudiantComponent pour les Etudiant de la liste
        /// </summary>
        /// <param name="listEtudiants">liste des etudiants à afficher</param>
        /// <param name="typetri">type de tri</param>
        /// <author>Nordine</author>
        private void AfficherLesEtuComponent(List<Etudiant> listEtudiants, TYPETRI ?typetri)
        {
            // On réinitialise le StackPanel
            StackPanelEtudiants.Children.Clear();

            // Affiche la liste triée dans l'ordre croissant ou non du typetri choisi (par defaut NumApogee)
            switch (typetri)
            {
                case TYPETRI.PRENOM:
                    listEtudiants = TriCroissant ?
                        listEtudiants.OrderByDescending(etudiant => etudiant.Prenom).ToList() :
                        listEtudiants.OrderBy(etudiant => etudiant.Prenom).ToList();
                    break;
                case TYPETRI.NOM:
                    listEtudiants = TriCroissant ?
                        listEtudiants.OrderByDescending(etudiant => etudiant.Nom).ToList() :
                        listEtudiants.OrderBy(etudiant => etudiant.Nom).ToList();
                    break;
                case TYPETRI.GROUPE:
                    listEtudiants = TriCroissant ?
                        listEtudiants.OrderByDescending(etudiant => etudiant.Groupe).ToList() :
                        listEtudiants.OrderBy(etudiant => etudiant.Groupe).ToList();
                    break;
                case TYPETRI.APOGEE:
                    listEtudiants = TriCroissant ?
                        listEtudiants.OrderByDescending(etudiant => etudiant.NumApogee).ToList() :
                        listEtudiants.OrderBy(etudiant => etudiant.NumApogee).ToList();
                    break;
                default:
                    listEtudiants = TriCroissant ?
                        listEtudiants.OrderByDescending(etudiant => etudiant.NumApogee).ToList() :
                        listEtudiants.OrderBy(etudiant => etudiant.NumApogee).ToList();
                    break;
            }

            foreach (Etudiant etu in listEtudiants)
            {
                // Si l'étudiant n'est pas déjà dans le StackPanel, on l'y ajoute
                if (!StackPanelEtudiants.Children.OfType<EtudiantComponent>().Any(uc => uc.NumeroApogee == etu.NumApogee))
                {
                    // Ajoute l'EtudiantComponent personnalisé au StackPanel
                    EtudiantComponent EtudiantComponent = new EtudiantComponent(etu);
                    StackPanelEtudiants.Children.Add(EtudiantComponent);
                }
            }
        }

        /// <summary>
        /// Affiche la liste des etudiants filtré
        /// </summary>
        /// <param name="listEtudiants">liste des étudiants a filtrer</param>
        /// <param name="filterType">type de filtre</param>
        /// <param name="filterText">texte saisi pour filtrer</param>
        /// <author>Nordine</author>
        private void AfficherLesEtuComponentFiltre(List<Etudiant> listEtudiants, TYPETRI filterType, string filterText)
        {
            // On réinitialise le StackPanel
            StackPanelEtudiants.Children.Clear();

            // Applique le filtre sur la liste d'étudiants
            List<Etudiant> filteredList = (List<Etudiant>)listEtudiants.Where(GetFilter(filterType, filterText)).ToList();

            if(String.IsNullOrEmpty(filterText))
            {
                ChargementDiffereInitial();
            }
            else
                etudiantAffichage = filteredList;


            AfficherLesEtuComponent(filteredList, null);
        }


        /// <summary>
        /// Inverse le bool de l'ordre de tri (par prenom)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OrderByPrenom(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les etudiants dans le bonne ordres
            AfficherLesEtuComponent(etudiantAffichage, TYPETRI.PRENOM);
        }

        /// <summary>
        /// Inverse le bool de l'ordre de tri (par nom)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OrderByNom(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les etudiants dans le bonne ordres
            AfficherLesEtuComponent(etudiantAffichage, TYPETRI.NOM);
        }

        /// <summary>
        /// Inverse le bool de l'ordre de tri (par num apogee)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OrderByApogee(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les etudiants dans le bonne ordres
            AfficherLesEtuComponent(etudiantAffichage, TYPETRI.APOGEE);
        }

        /// <summary>
        /// Inverse le bool de l'ordre de tri (par groupe)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OrderByGroupe(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les etudiants dans le bonne ordres
            AfficherLesEtuComponent(etudiantAffichage, TYPETRI.GROUPE);
        }


        /// <summary>
        /// Quand on change le filtre selectionner dans la combobox des filtres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void SelectionFiltreChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //on recupere le filtre selectionner dans la combobox
            TYPETRI filterType = TYPETRI.APOGEE;
            switch (ComboBoxFiltre.SelectedIndex)
            {
                case 1:
                    filterType = TYPETRI.NOM;
                    break;
                case 0:
                    filterType = TYPETRI.PRENOM;
                    break;
                case 2:
                    filterType = TYPETRI.GROUPE;
                    break;

            }
            //on recupere le string saisi dans le textbox
            string filterText = TexteFiltre.Text;

            // Appel de la méthode avec le filtre sélectionné
            AfficherLesEtuComponentFiltre(etudiants.ListeEtu, filterType, filterText);
        }

        /// <summary>
        /// Quand le texte du filtre/Recherche a changer on mets a jour l'affichage des etudiants avec ce nouveau filtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void TexteFiltreChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //si un filtre du combobox a été selectionner
            if(ComboBoxFiltre.SelectedIndex!=-1)
            {
                //on recupere le filtre selectionner dans la combobox
                TYPETRI filterType = TYPETRI.APOGEE;
                switch (ComboBoxFiltre.SelectedIndex)
                {
                    case 1:
                        filterType = TYPETRI.NOM;
                        break;
                    case 0:
                        filterType = TYPETRI.PRENOM;
                        break;
                    case 2:
                        filterType = TYPETRI.GROUPE;
                        break;

                }
                //on recupere le string saisi dans le textbox
                string filterText = TexteFiltre.Text;

                // Appel de la méthode avec le filtre sélectionné
                AfficherLesEtuComponentFiltre(etudiantAffichage, filterType, filterText);
            }

        }


        /// <summary>
        /// Renvoi le filtre d'étudiant 
        /// </summary>
        /// <param name="filterType">type de filtre choisi (nom, prenom...)</param>
        /// <param name="filterText">Texte saisi pour filtre</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        private Func<Etudiant, bool>? GetFilter(TYPETRI filterType, string filterText)
        {
            Func<Etudiant, bool> filter = null;
            switch (filterType)
            {
                case TYPETRI.PRENOM:
                    filter = etudiant => etudiant.Prenom.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                    break;
                case TYPETRI.NOM:
                    filter = etudiant => etudiant.Nom.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                    break;
                case TYPETRI.GROUPE:
                    filter = etudiant => etudiant.Groupe.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase);
                    break;
                case TYPETRI.APOGEE:
                    filter = etudiant => etudiant.NumApogee.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase);
                    break;
            }
            return filter;
        }

        private void OpenGestionUtilisateur(object sender, RoutedEventArgs e)
        {
            if (users != null)
            {
                GestionUtilisateurs gestionUtilisateurs = new GestionUtilisateurs();
                gestionUtilisateurs.Show();
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
        /// Ouvre la fenetre avec les étudiants qui ont une certaine categorie de note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ClickAfficherEtudiantByCategorie(object sender, RoutedEventArgs e)
        {
            FenetreEtudiantByCategorie fenetreEtudiantByCategorie = new FenetreEtudiantByCategorie(this.etudiants,this.promo);
            fenetreEtudiantByCategorie.Show();

            this.Close();
        }
    }

    /// <summary>
    /// Différent élément qu'on peut utiliser pour trier
    /// </summary>
    /// <author>Nordine</author>
    public enum TYPETRI
    {
        PRENOM,NOM,GROUPE,APOGEE,NBNOTE,LOGIN
    }

}

