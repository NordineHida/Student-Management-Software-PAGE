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
    /// Logique d'interaction pour InformationsSupplementaires.xaml
    /// </summary>
    public partial class InformationsSupplementaires : Window
    {
        private Etudiant etudiant;

        /// <summary>
        /// Constructeur qui prend l'étudiant selectionné avec le double clique
        /// </summary>
        /// <param name="EtudiantActuel">etudiant actuel</param>
        /// <author>Yamato</author>
        public InformationsSupplementaires(Etudiant EtudiantActuel)
        {
            InitializeComponent();
            etudiant = EtudiantActuel;
            ChargerInfosImpEtudiant();
            ChargementDiffereNotes();
        }

        /// <summary>
        /// Charge les informations importantes de l'étudiant
        /// </summary>
        /// <author>Yamato</author>
        public void ChargerInfosImpEtudiant()
        {
            txtName.Text = etudiant.Nom;
            txtPrenom.Text = etudiant.Prenom;
            txtNumApogee.Text = etudiant.NumApogee.ToString();
            txtGroupe.Text = etudiant.Groupe;
            txtMail.Text = etudiant.Mail;
            txtSexe.Text = etudiant.Sexe.ToString();
            txtTypebac.Text = etudiant.TypeBac;
            txtBoursier.Text = etudiant.EstBoursier ? "Oui" : "Non";
            txtRegime.Text = etudiant.TypeFormation;
        }

        /// <summary>
        /// Charge les informations complémentaires de l'étudiant
        /// </summary>
        /// <author>Yamato</author>
        public void ChargerInfosCompEtudiant()
        {
            txtDateNaissance2.Text = etudiant.DateNaissance.ToString();
            txtAdresse2.Text = etudiant.Adresse;
            txtTelFixe2.Text = etudiant.TelFixe.ToString();
            txtTelPortable2.Text = etudiant.TelPortable.ToString();
            txtLogin2.Text = etudiant.Login;
            
        }


        /// <summary>
        /// Rend visible les informations complétementaires lors du clique sur le bouton ou les rend invisibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Yamato</author>
        private void InfosComp_Click(object sender, RoutedEventArgs e)
        {
            if (contInfosComp.Visibility == Visibility.Collapsed)
            {
                ChargerInfosCompEtudiant();
                contInfosComp.Visibility = Visibility.Visible;
            }
            else
            {
                contInfosComp.Visibility = Visibility.Collapsed;
                
            }
        }

        /// <summary>
        /// Chargement des notes différé via l'API
        /// </summary>
        /// <author>Laszlo</author>
        private async Task ChargementDiffereNotes()
        {
            //On reinitialise la liste
            maListViewNote.Items.Clear();

            //On récupere l'ensemble des étudiants via l'API
            List<Note> notes = (await EtuDAO.Instance.GetAllNotesByApogee(etudiant.NumApogee)).ToList();

            foreach (Note note in notes)
            {
                //Si l'étudiant est pas déjà dans la liste on l'y ajoute
                if (!maListViewNote.Items.Contains(note))
                    maListViewNote.Items.Add(note);
            }
        }

        /// <summary>
        /// Supprime une note via l'API
        /// </summary>
        /// <author>Laszlo</author>
        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            if (maListViewNote.SelectedItem != null)
            {
                // Obtenez l'étudiant sélectionné dans la ListView
                Note noteSelectionne = maListViewNote.SelectedItem as Note;
                if (noteSelectionne != null)
                {
                   EtuDAO.Instance.DeleteNote(noteSelectionne);
                }
            }
        }

        /// <summary>
        /// Ouvre une fenêtre affichant la note lorsqu'on double clique sur la note
        /// </summary>
        /// <author>Laszlo</author>
        private void maListView_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (maListViewNote.SelectedItem != null)
            {
                // Obtenez l'étudiant sélectionné dans la ListView
                Note noteSelectionne = maListViewNote.SelectedItem as Note;

                if (noteSelectionne != null)
                {
                    // Créez une instance de la fenêtre InformationsSupplementaires en passant l'étudiant sélectionné en paramètre
                    AffichageNote affichageNote = new AffichageNote(noteSelectionne);
                    affichageNote.Show();
                }
            }
        }
        
    }
}
