using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Model.PatternObserveur;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Token token;

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

        /// <summary>
        /// Constructeur de la page (initialise les éléments)
        /// </summary>
        /// <author>Nordine/Yamato</author>
        public ChoixPromo(Token? tokenUtilisateur)
        {
            InitializeComponent();
            MettreAJourComboBox();

            ComboBoxAnnee.ItemsSource = annees;
            ComboBoxAnnee.DisplayMemberPath = "AnneeDebut";

            this.token = tokenUtilisateur;
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

                FenetrePrincipal fenetrePrincipal;
                if (token != null)
                {
                    fenetrePrincipal = new FenetrePrincipal(new Promotion(np, anneeeSelection.AnneeDebut), token);
                }
                else
                {
                    fenetrePrincipal = new FenetrePrincipal(new Promotion(np, anneeeSelection.AnneeDebut), null);
                }
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
        /// <author>Yamato/Nordine</author>        
        private async void AjouterAnnee(object sender, RoutedEventArgs e)
        {
            // On ouvre une nouvelle fenêtre permettant la saisie de l'année à ajouter
            AjoutAnnee ajoutAnnee = new AjoutAnnee();
            ajoutAnnee.ShowDialog();

            //si l'année saisie est correct
            if (ajoutAnnee.AnneeSaisie >1800)
            {
                // On créer une nouvelle année avec l'année saisie
                Annee nouvelleAnnee = new Annee(ajoutAnnee.AnneeSaisie);

                // On l'ajoute à la liste d'années
                annees.Add(nouvelleAnnee);

                // Création de l'année 
                AnneeDAO dao = new AnneeDAO();
                dao.CreateAnnee(nouvelleAnnee.AnneeDebut);

                //on remet a jour la combobox
                await Task.Delay(1000);
                MettreAJourComboBox();
            }

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
