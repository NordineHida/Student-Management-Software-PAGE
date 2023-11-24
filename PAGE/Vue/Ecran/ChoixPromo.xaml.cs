using PAGE.Model;
using PAGE.Model.Enumerations;
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
        private Promotion promotion;

        /// <summary>
        /// Renvoie une liste d'année
        /// </summary>
        /// <author>Yamato</author>
        public List<Annee> Annees { get { return annees; } }

        /// <summary>
        /// Renvoie ou définit la promotion selectionnée dans la combobox
        /// </summary>
        /// <author>Yamato</author>
        public Promotion Promotion { get { return promotion; } set { promotion = value; } }

        public ChoixPromo()
        {
            InitializeComponent();
            MettreAJourComboBox();

            ComboBoxAnnee.ItemsSource = annees;
            ComboBoxAnnee.DisplayMemberPath = "AnneeDebut";
        }

        /// <summary>
        /// Clique bouton valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ValiderChoixPromo(object sender, RoutedEventArgs e)
        {
            if (ComboBoxAnnee.SelectedIndex == -1 || ComboBoxPromotion.SelectedIndex == -1)
            {
                PopUp popUp = new PopUp("Erreur de selection", "Veuillez séléctionner une année et une promotion", TYPEICON.ERREUR);
                popUp.ShowDialog();
            }
            else
            {
                // Prendre l'année selectionné
                NOMPROMOTION np;
                switch (ComboBoxPromotion.SelectedIndex)
                {
                    case 0:
                        np = NOMPROMOTION.BUT1;
                        break;
                    case 1:
                        np = NOMPROMOTION.BUT2;
                        break;
                    default:
                        np = NOMPROMOTION.BUT3;
                        break;
                }

                
                Annee anneeeSelection = (Annee)ComboBoxAnnee.Items[ComboBoxAnnee.SelectedIndex];



                FenetrePrincipal fenetrePrincipal = new FenetrePrincipal(new Promotion(np,anneeeSelection.AnneeDebut));
                fenetrePrincipal.Show();

                this.Close();
            }


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
        /// Bouton ouvrant une nouvelle fenetre permettant d'ajouter une année
        /// </summary>
        /// <author>Yamato</author>        
        private void AjouterAnnee(object sender, RoutedEventArgs e)
        {
            // On ouvre une nouvelle fenêtre permettant la saisie de l'année à ajouter
            AjoutAnnee ajoutAnnee = new AjoutAnnee();
            ajoutAnnee.ShowDialog();

            // On créer une nouvelle année avec l'année saisie
            Annee nouvelleAnnee = new Annee(ajoutAnnee.AnneeSaisie);

            // On l'ajoute à la liste d'années
            annees.Add(nouvelleAnnee);

            // Création de l'année 
            AnneeDAO dao = new AnneeDAO();
            dao.CreateAnnee(nouvelleAnnee.AnneeDebut);
        }

        private void SupprimerAnnee(object sender, RoutedEventArgs e)
        {
            Annee anneeSelectionnee = ComboBoxAnnee.SelectedItem as Annee;

            if (anneeSelectionnee != null)
            {
                // Supprimez l'année de la liste
                annees.Remove(anneeSelectionnee);

                // Mettez à jour la ComboBox
                ComboBoxAnnee.ItemsSource = null;
                ComboBoxAnnee.ItemsSource = annees;
                ComboBoxAnnee.DisplayMemberPath = "AnneeDebut";

                // Supprimez l'année de la base de données
                AnneeDAO dao = new AnneeDAO();
                dao.DeleteAnnee(anneeSelectionnee.AnneeDebut);
            }
        }
    }
}
