using PAGE.APIEtudiant.Stockage;
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

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour CreationNote.xaml
    /// </summary>
    public partial class CreationNote : Window
    {
        private Note note;
        /// <summary>
        /// Constructeur de fenêtre CreationNote
        /// </summary>
        /// <param name="note"></param>
        /// <author>Laszlo</author>
        public CreationNote(Note note)
        {
            InitializeComponent();
            DataContext = note;
            this.note = note;
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

            //on crée la note
            if (isCreateOk(note))
            {
                EtuDAO.Instance.CreateNote(note);
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


        public bool isCreateOk(Note note)
        {
            bool valide = true;
            if (note == null)valide = false;
            else if (note.Categorie == null || note.Nature == null) valide = false;
            return valide;
        }
    }
}
