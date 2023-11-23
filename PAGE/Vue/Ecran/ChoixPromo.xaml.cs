using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour ChoixPromo.xaml
    /// </summary>
    public partial class ChoixPromo : Window
    {
        private List<Annee> annees = new List<Annee>();


        public ChoixPromo()
        {
            InitializeComponent();

            if (ComboBoxAnnee != null)
            {
                ComboBoxAnnee.ItemsSource = annees;
                ComboBoxAnnee.DisplayMemberPath = "AnneeDebut";
            }

        }

        /// <summary>
        /// Clique bouton valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ValiderChoixPromo(object sender, RoutedEventArgs e)
        {
            FenetrePrincipal fenetrePrincipal = new FenetrePrincipal();
            fenetrePrincipal.Show();

            this.Close();
        }

        /// <summary>
        /// Mets à jour la combobox en triant de la plus petite année à la plus grande
        /// </summary>
        /// <author>Yamato</author>
        private void MettreAJourComboBox()
        {
            // Tri de la liste des années par ordre croissant
            var anneesTriees = annees.OrderBy(a => a.AnneeDebut).ToList();

            ComboBoxAnnee.ItemsSource = anneesTriees;
        }

        /// <summary>
        /// Bouton ouvrant une nouvelle fenetre permettant d'ajouter une année
        /// </summary>
        /// <author>Yamato</author>        
        private void AjouterAnnee(object sender, RoutedEventArgs e)
        {
            // On ouvre une nouvelle fenêtre permettant la saisie de l'année à ajouter
            AjoutAnnee ajoutAnnee = new AjoutAnnee();
            ajoutAnnee.ShowDialog();

            // Ajout de l'année dans la liste que si celle-ci est valide
            if (int.TryParse(ajoutAnnee.AnneeSaisie, out int anneeSaisie))
            {
                // Obtention de l'année actuelle
                int anneeActuelle = DateTime.Now.Year;

                // Vérifiez si l'année saisie est supérieure à l'année actuelle
                if (anneeSaisie <= anneeActuelle)
                {
                    // On créer une nouvelle année avec l'année saisie
                    Annee nouvelleAnnee = new Annee(ajoutAnnee.AnneeSaisie, null, null, null);

                    // On l'ajoute à la liste d'années
                    annees.Add(nouvelleAnnee);

                    // Création de l'année 
                    AnneeDAO dao = new AnneeDAO();
                    dao.CreateAnnee(nouvelleAnnee);

                    MettreAJourComboBox();
                }
                
            }

        }
    }
}
