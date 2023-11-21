using PAGE.Model.PatternObserveur;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Linq;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetrePrincipal.xaml
    /// </summary>
    public partial class FenetrePrincipalBis : Window, IObservateur
    {
        private UIElement initialContent;
        private Etudiants etudiants;

        /// <summary>
        /// Initialise la fenetre principal
        /// </summary>
        /// <author>Nordine & Stephane</author>
        public FenetrePrincipalBis()
        {
            InitializeComponent();

            initialContent = (UIElement?)this.Content;

            ChargementDiffere();
        }


        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        /// <author>Nordine & Stephane</author>
        private async Task ChargementDiffere()
        {
            // On reinitialise le StackPanel
            StackPanelEtudiants.Children.Clear();

            // On récupère l'ensemble des étudiants via l'API
            EtuDAO dao = new EtuDAO();
            this.etudiants = new Etudiants((List<Etudiant>)await dao.GetAllEtu());

            foreach (Etudiant etu in etudiants.ListeEtu)
            {
                // Si l'étudiant n'est pas déjà dans le StackPanel, on l'y ajoute
                if (!StackPanelEtudiants.Children.OfType<EtudiantComponent>().Any(uc => uc.NumeroApogee == etu.NumApogee))
                {
                    // Créez et ajoutez votre EtudiantComponent personnalisé au StackPanel
                    EtudiantComponent EtudiantComponent = new EtudiantComponent(etu);
                    StackPanelEtudiants.Children.Add(EtudiantComponent);
                }
            }

            // On enregistre cette fenetre comme observeur des notes
            etudiants.Register(this);
        }



        /// <summary>
        /// Ouvre la fenetre de connexion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenLoginPage(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();

            this.Close();
        }
        /// <summary>
        /// Ouvre la fenêtre des paramètres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenParametresPage(object sender, RoutedEventArgs e)
        {
            ParametrePage parametre = new ParametrePage();
            parametre.Show();

            this.Close();
        }

        private void ParamPage_ReturnToMainWindow(object sender, EventArgs e)
        {
            this.Content = initialContent;
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
                dao.AddSeveralEtu(lc.GetEtudiants(selectedFilePath));
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
                FenetreCreerEtudiant fenetreCreerEtudiant = new FenetreCreerEtudiant(etudiants);
                fenetreCreerEtudiant.Show();
            }
            else
            {
                PopUp popUp = new PopUp("Création", "Veuillez attendre la fin du chargement des étudiants", TYPEICON.INFORMATION);
                popUp.ShowDialog();
            }

        }

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
        /// <author>Yamato</author>
        private void EtudiantComponent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is EtudiantComponent EtudiantComponent)
            {
                // On recupère l'étudiant associé au EtudiantComponent
                Etudiant etudiantSelectionne = EtudiantComponent.Etudiant;

                if (etudiantSelectionne != null)
                {
                    // on affiche ces informations
                    InformationsSupplementaires informationsSupplementaires = new InformationsSupplementaires(etudiantSelectionne, etudiants);
                    informationsSupplementaires.Show();
                }
            }
        }


    }
}

