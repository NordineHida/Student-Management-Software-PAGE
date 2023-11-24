using PAGE.Model.PatternObserveur;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour FenetreEtudiantByCategorie.xaml
    /// </summary>
    public partial class FenetreEtudiantByCategorie : Window, IObservateur
    {
        private Etudiants etudiants;
        private List<Etudiant> etudiantAffichage;
        private List<Tuple<Etudiant, int>> etudiantEtNote;
        private bool TriCroissant = false;
        private CATEGORIE categorieComboBox;

        public FenetreEtudiantByCategorie(Etudiants etudiants)
        {
            InitializeComponent();
            this.etudiants = etudiants;
            etudiantEtNote = new List<Tuple<Etudiant, int>>();
        }


        /// <summary>
        /// Chargement des etudiants différé via l'API
        /// </summary>
        /// <author>Nordine</author>
        private async Task ChargementDiffere()
        {
            // On récupère l'ensemble des étudiants et leur nbNote via l'API
            EtuDAO dao = new EtuDAO();
            etudiantEtNote = await dao.GetAllEtuByCategorie(categorieComboBox);
;
            //await dao.GetAllEtuByCategorie(categorieComboBox);
            this.etudiants = new Etudiants(etudiantEtNote.Select(tuple => tuple.Item1).ToList());

            etudiantAffichage = etudiants.ListeEtu;

            //Affiche les components des etudiants (trie par numero apogee par defaut
            AfficherLesEtuComponent(etudiants.ListeEtu, TYPETRI.APOGEE);

            // On enregistre cette fenetre comme observeur des etudiants
            etudiants.Register(this);
        }


        /// <summary>
        /// Une modification a ete recu, on raffraichis l'affichage
        /// </summary>
        /// <param name="Message">message specifique</param>
        /// <author>Nordine</author>
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
            if (sender is EtudiantEtNoteComponent etudiantComponent)
            {
                // On recupère l'étudiant associé au EtudiantComponent
                Etudiant etudiantSelectionne = etudiantComponent.Etudiant;

                if (etudiantSelectionne != null)
                {
                    // on affiche ces informations
                    InformationsSupplementaires informationsSupplementaires = new InformationsSupplementaires(etudiantSelectionne, etudiants);
                    informationsSupplementaires.Show();
                }
            }
        }

        /// <summary>
        /// Affiche les EtudiantEtNoteComponent pour les Etudiant de la liste
        /// </summary>
        /// <param name="listEtudiants">liste des etudiants à afficher</param>
        /// <param name="typetri">type de tri</param>
        /// <author>Nordine</author>
        private void AfficherLesEtuComponent(List<Etudiant> listEtudiants, TYPETRI? typetri)
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
                case TYPETRI.NBNOTE:
                    listEtudiants = TriCroissant ?
                        etudiantEtNote.OrderBy(tuple => tuple.Item2).Select(tuple => tuple.Item1).ToList() :
                        etudiantEtNote.OrderByDescending(tuple => tuple.Item2).Select(tuple => tuple.Item1).ToList();
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
                if (!StackPanelEtudiants.Children.OfType<EtudiantEtNoteComponent>().Any(uc => uc.NumeroApogee == etu.NumApogee))
                {
                    // Ajoute l'EtudiantEtNoteComponent personnalisé au StackPanel
                    EtudiantEtNoteComponent EtudiantComponent = new EtudiantEtNoteComponent(etu, etudiantEtNote.FirstOrDefault(tuple => tuple.Item1.Equals(etu))?.Item2 ?? -1); //Mets -1 s'il ne trouve pas l'étudiant dans la liste  de tuple
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
            List<Etudiant> filteredList = listEtudiants.Where(GetFilter(filterType, filterText)).ToList();

            if (String.IsNullOrEmpty(filterText))
            {
                ChargementDiffere();
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
        /// Trie par nombre de notes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void OrderByNbNote(object sender, RoutedEventArgs e)
        {
            // Inversion de la valeur de TriCroissant
            TriCroissant = !TriCroissant;

            //on raffiche les etudiants dans le bonne ordres
            AfficherLesEtuComponent(etudiantAffichage, TYPETRI.NBNOTE);
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
            AfficherLesEtuComponentFiltre(etudiantEtNote.Select(tuple => tuple.Item1).ToList(), filterType, filterText);


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
            if (ComboBoxFiltre.SelectedIndex != -1)
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
                AfficherLesEtuComponentFiltre(etudiantEtNote.Select(tuple => tuple.Item1).ToList(), filterType, filterText);
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


        /// <summary>
        /// Ouvre la fenetre principal et ferme la fenetre actuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void FermerFenetre(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FenetrePrincipal fp = new FenetrePrincipal();
            fp.Show();
        }

        /// <summary>
        /// Quand on change la combobox des categorie, chante l'attribut categorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxCategorie_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (ComboBoxCategorie.SelectedIndex)
            {
                case 0:
                    categorieComboBox = CATEGORIE.ABSENTEISME;
                    break;
                case 1:
                    categorieComboBox = CATEGORIE.PERSONNEL;
                    break;
                case 2:
                    categorieComboBox = CATEGORIE.MEDICAL;
                    break;
                case 3:
                    categorieComboBox = CATEGORIE.RESULTATS;
                    break;
                case 4:
                    categorieComboBox = CATEGORIE.ORIENTATION;
                    break;
                case 5:
                    categorieComboBox = CATEGORIE.AUTRE;
                    break;
            }

            ChargementDiffere();
        }


    }
}
