using PAGE.Stockage;
using PAGE.Model;
using System;
using System.Collections.Generic;
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
using PAGE.Model.PatternObserveur;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour CreationNote.xaml
    /// </summary>
    public partial class CreationNote : Window
    {
        private Note note;
        private Notes notes;
        private bool modeCreation;

        /// <summary>
        /// Constructeur de fenêtre CreationNote
        /// </summary>
        /// <param name="note"></param>
        /// <author>Laszlo / Lucas</author>
        public CreationNote(Note note, Notes notes)
        {
            InitializeComponent();

            DataContext = note;
            this.note = note;
            this.notes = notes;

            MiseAJourDateLabel();

            //on utilise un bool pour savoir si on doit créer une note ou simplement l'afficher
            modeCreation = true;
            if (note.Categorie != "")
            {
                modeCreation = false;

                Titre.Content = "Note :";

                BoutonCreer.Visibility = Visibility.Collapsed;
                BoutonModifier.Visibility = Visibility.Visible;
                //les switchs permettent d'afficher la valeur actuelle dans chaque comboBox 
                switch (note.Categorie)
                {
                    case "Absentéisme":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[0];
                        break;
                    case "Personnel":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[1];
                        break;
                    case "Médical":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[2];
                        break;
                    case "Résultats":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[3];
                        break;
                    case "Orientation":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[4];
                        break;
                    case "Autre":
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[5];
                        break;
                }

                switch (note.Nature)
                {
                    case "Mail":
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[0];
                        break;
                    case "Rdv":
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[1];
                        break;
                    case "Lettre":
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[2];
                        break;
                    case "Appel":
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[3];
                        break;
                    case "Autre":
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[5];
                        break;
                }



                ComboBoxConfidentialite.IsEnabled = false;
                ComboBoxCategorie.IsEnabled = false;
                ComboBoxNature.IsEnabled = false;
                TextCommentaire.IsReadOnly = true;   
            }


        }

        /// <summary>
        /// Méthode renvoyant la note crée quand on clique sur le bouton Créer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo</author>
        private void ClickCreer(object sender, RoutedEventArgs e)
        {
            //si on a choisi une catégorie
            if (ComboBoxCategorie.SelectedItem != null)
            { 
                //récupère la catègorie de la combobox et met la propriété "Catégorie" de la note à sa valeur
                ComboBoxItem categorieChoisie =(ComboBoxItem)ComboBoxCategorie.SelectedItem;
                string categorieChoisieString = categorieChoisie.ToString();
                string[] motsCatChoisie = categorieChoisieString.Split(": ");
                note.Categorie=motsCatChoisie[1];
            }
            //Si on n'a pas choisi de catégorie, un message s'affiche
            else { MessageBox.Show("Veuillez choisir une catégorie"); }

            if (ComboBoxNature.SelectedItem != null)
            {
                //récupère la catègorie de la combobox et met la propriété "Catégorie" de la note à sa valeur
                ComboBoxItem natureChoisie = (ComboBoxItem)ComboBoxNature.SelectedItem;
                string NatureChoisieString = natureChoisie.ToString();
                string[] motsNatChoisie = NatureChoisieString.Split(": ");
                note.Nature = motsNatChoisie[1];
            }
            //Si on n'a pas choisi de nature, un message s'affiche
            else { MessageBox.Show("Veuillez choisir une nature"); }
            note.DatePublication = DateCreationNote.SelectedDate.Value;
            //on crée la note
            if (isCreateOk(note))
            {
                EtuDAO.Instance.CreateNote(note);
                notes.AddNote(note);
                this.Close();
            }
            else { MessageBox.Show("Tous les champs ne sont pas corrects"); }
        }

        /// <summary>
        /// Méthode fermant la fenêtre sans renvoyer la note quand on clique sur le bouton annuler
        /// </summary>
        /// <author>Laszlo</author>
        private void ClickAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// méthode permettant de gérer le clic sur le bouton modifier une note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void ClickModify(object sender, RoutedEventArgs e)
        {
            BoutonValider.Visibility = Visibility.Visible;
            BoutonModifier.Visibility = Visibility.Collapsed;

            Titre.Content = "Modification de note";

            ComboBoxConfidentialite.IsEnabled = true;
            ComboBoxCategorie.IsEnabled = true;
            ComboBoxNature.IsEnabled = true;
            TextCommentaire.IsReadOnly = false;

        }

        /// <summary>
        /// Bouton Valider la modification de la note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas</author>
        private void ClickValider(object sender, RoutedEventArgs e)
        {
            BoutonValider.Visibility = Visibility.Collapsed;
            BoutonModifier.Visibility = Visibility.Visible;

            Titre.Content = "Note";

            ComboBoxConfidentialite.IsEnabled = false;
            ComboBoxCategorie.IsEnabled = false;
            ComboBoxNature.IsEnabled = false;
            TextCommentaire.IsReadOnly = true;

        }

        /// <summary>
        /// Définit si l'on peut créer la note au moment de valider
        /// </summary>
        /// <param name="note">note à créer</param>
        /// <returns>true si elle est correcte, faux sinon</returns>
        /// <author>Laszlo</author>
        public bool isCreateOk(Note note)
        {
            bool valide = true;
            if (note == null) valide = false;
            else if (note.Categorie == null || note.Nature == null) valide = false;
            else if (note.Commentaire.Contains('\''))
            {
                for (int i = 0; i < note.Commentaire.Length; i++)
                {
                    if (note.Commentaire[i] == '\'')
                    {
                        note.Commentaire.Insert(i, "\'");
                    }
                }
            }
            else if (note.DatePublication > DateTime.Now) return false;
            return valide;
        }

        private void MiseAJourDateLabel()
        {
            DateTime dateNote = this.note.DatePublication;

            Date.Content = "Date : " + dateNote.ToString("dd/MM/yyyy");
        }

    }
}
