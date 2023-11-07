﻿using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using PAGE.APIEtudiant.Stockage;
using PAGE.Model;
using PAGE.Vue.Ressources;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour FenetrePrincipal.xaml
    /// </summary>
    public partial class FenetrePrincipal : Window
    {
        private UIElement initialContent;

        /// <summary>
        /// Initialise la fenetre principal
        /// </summary>
        /// <author>Nordine & Stephane</author>
        public FenetrePrincipal()
        {
            InitializeComponent();

            initialContent = (UIElement?)this.Content;

            ChargementDiffere();

        }

        /// <summary>
        /// trie la liste a partir de la liste cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Stéphane</author>
        private bool isSortAscending = true;


        #region trie 
        /// <summary>
        /// code pour trié par ordre croisant et décroisant + recherche détudiant.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <author>Stephane</author>
        private void Trie_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);

            string sortBy = column.Tag.ToString();

            // Vérifie s'il existe des descriptions de tri pour les éléments de la liste.
            if (maListView.Items.SortDescriptions.Count > 0)
            {
                // Efface les descriptions de tri existantes.
                maListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir;

            // Détermine la direction de tri en fonction de la valeur de 'isSortAscending'.
            // Si 'isSortAscending' est vrai, le tri est défini sur croissant, sinon sur décroissant.
            // Inverse ensuite la valeur de 'isSortAscending'.
            if (isSortAscending)
            {
                newDir = ListSortDirection.Ascending; //trie croisant 
                isSortAscending = false;
            }
            else
            {
                newDir = ListSortDirection.Descending; //trie décroisant
                isSortAscending = true;
            }

            // Ajouter une nouvelle description de tri à la liste 
            maListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

            //lie maListeView a ListView
            maListView.ItemsSource = maListView.Items; 


        }

        private Predicate<object> GetFilter()
        {

            Predicate<object> resultat = null;

            switch (FilterBy.SelectedIndex)
            {
                case 0: // "Nom"
                    resultat = NameFilter;
                    break;
                case 1: // "Prenom"
                    resultat = FirstNameFilter;
                    break;
                default: // trie de base par nom
                    resultat = NameFilter;
                    break;
            }

            return resultat;

        }
        // La fonction utilise les nom pour filtrer les Etudiant.
        private bool NameFilter(object obj) 
        {
            var Filterobj = obj as Etudiant;
            return Filterobj.Nom.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }


        // La fonction utilise les prenom pour filtrer les Etudiant.
        private bool FirstNameFilter(object obj)
        {
            var Filterobj = obj as Etudiant;
            return Filterobj.Prenom.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }



        // Et utilisé quand la sélection de l'élément change dans le contrôle de sélection FilterBy.
        // Elle met à jour le filtre appliqué fonction du choix de l'utilisateur.
        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Applique le filtre approprié dans maListView en fonction du choix de l'utilisateur.
            maListView.Items.Filter = GetFilter();

        }


        // Et utilisé quand un événement TextChanged est déclenché par le contrôle FilterTextBox.
        // Elle met à jour le filtre appliqué à la collection d'objets affichée dans maListView en fonction du texte entré par l'utilisateur.
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(FilterTextBox.Text == null)
            {
                // Si le texte est vide, supprimer tout filtre appliqué à la collection d'objets de maListView.
                maListView.Items.Filter = null;
            }
            else
            {
                // Si un texte est présent, appliquer le filtre approprié dans maListView en fonction du texte saisi par l'utilisateur.
                maListView.Items.Filter = GetFilter();
            }
        }
        #endregion

        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        /// <author>Nordine & Stephane</author>
        private async Task ChargementDiffere()
        {
            //On reinitialise la liste
            maListView.Items.Clear();

            //On récupere l'ensemble des étudiants via l'API
            List<Etudiant> etudiants = (await EtuDAO.Instance.GetAllEtu()).ToList();

            foreach (Etudiant etu in etudiants)
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if (!maListView.Items.Contains(etu))
                    maListView.Items.Add(etu);
            }
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

        private void OpenParametresPage(object sender, RoutedEventArgs e)
        {
            // Créer la LoginPage
            ParametresPage parampage = new ParametresPage();

            // Écouter l'événement de retour à la fenêtre principale
            parampage.ReturnToMainWindow += ParamPage_ReturnToMainWindow;

            // Afficher la LoginPage comme contenu initial
            this.Content = parampage;
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
                EtuDAO.Instance.AddSeveralEtu(lc.GetEtudiants(selectedFilePath));
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

            //On ouvre une pop-up pour indiquer qu'on a bien actualiser
            MessageBox.Show("Vous avez bien actualisé", "Actualisation avec succès", MessageBoxButton.OK);
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

        
    }
}

