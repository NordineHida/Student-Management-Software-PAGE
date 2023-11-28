using PAGE.Stockage;
using PAGE.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using PAGE.Model.Enumerations;
using DocumentFormat.OpenXml.Vml.Office;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour CreationNote.xaml
    /// </summary>
    public partial class CreationNote : Window
    {
        private Note note;
        private Notes notes;
        private Token token;
        private Promotion promo;

        /// <summary>
        /// Constructeur de fenêtre CreationNote
        /// </summary>
        /// <param name="note">note à créer/afficher/modifier</param>
        /// <param name="notes">liste des notes existantes</param>
        /// <param name="noteExiste">indique si la note a déjà été crée par cette fenêtre ou non</param>
        /// <author>Laszlo / Lucas / Nordine</author>
        public CreationNote(Note note, Notes notes, bool noteExist,Promotion promo, Token? tokenUtilisateur)
        {
            InitializeComponent();

            DataContext = note;
            this.note = note;
            this.notes = notes;
            this.promo = promo;
            this.token = tokenUtilisateur;
            
                
            //Si on est en mode affichage (la note existe)
            if (noteExist)
            {
                Titre.Content = "Note :";
                BoutonCreer.Visibility = Visibility.Collapsed;

                if (tokenUtilisateur != null)
                {
                    if (token.UserToken.Roles.ContainsKey(promo.AnneeDebut))
                    {
                        if (token.UserToken.Roles[promo.AnneeDebut] != ROLE.LAMBDA && token.UserToken.Roles[promo.AnneeDebut] != ROLE.ADMIN)
                        {
                            BoutonSupprimer.Visibility = Visibility.Visible;
                            BoutonModifier.Visibility = Visibility.Visible;
                        }
                    }
                }
                //les switchs permettent d'afficher la valeur actuelle dans chaque comboBox 
                switch (note.Categorie)
                {
                    case CATEGORIE.ABSENTEISME:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[0];
                        break;
                    case CATEGORIE.PERSONNEL:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[1];
                        break;
                    case CATEGORIE.MEDICAL:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[2];
                        break;
                    case CATEGORIE.RESULTATS:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[3];
                        break;
                    case CATEGORIE.ORIENTATION:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[4];
                        break;
                    case CATEGORIE.AUTRE:
                        ComboBoxCategorie.SelectedItem = ComboBoxCategorie.Items[5];
                        break;
                }

                switch (note.Nature)
                {
                    case NATURE.MAIL:
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[0];
                        break;
                    case NATURE.RDV:
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[1];
                        break;
                    case NATURE.LETTRE:
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[2];
                        break;
                    case NATURE.APPEL:
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[3];
                        break;
                    case NATURE.AUTRE:
                        ComboBoxNature.SelectedItem = ComboBoxNature.Items[4];
                        break;
                }

                switch (note.Confidentialite)
                {
                    case CONFIDENTIALITE.MEDICAL:
                        ComboBoxConfidentialite.SelectedIndex = 0;
                        break;
                    case CONFIDENTIALITE.CONFIDENTIEL:
                        ComboBoxConfidentialite.SelectedIndex = 1;
                        break;
                    case CONFIDENTIALITE.INTERNE:
                        ComboBoxConfidentialite.SelectedIndex = 2;
                        break;
                    case CONFIDENTIALITE.PUBLIC:
                        ComboBoxConfidentialite.SelectedIndex = 3;
                        break;
                }



                ComboBoxConfidentialite.IsEnabled = false;
                ComboBoxCategorie.IsEnabled = false;
                ComboBoxNature.IsEnabled = false;
                TextCommentaire.IsReadOnly = true;
                DateCreationNote.IsEnabled = false;

                DateCreationNote.SelectedDate = note.DatePublication;
            }
            else
            {
                //si la note n'existe pas, on met la date du jour par defaut
                DateCreationNote.SelectedDate = DateTime.Now;
                note.Commentaire = "";
                BoutonSupprimer.Visibility = Visibility.Collapsed;
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
            //si les informations sont correcte on créer la note
            if (isCreateOk(note))
            {
                NoteDAO dao = new NoteDAO();
                dao.CreateNote(note);
                notes.AddNote(note);
                this.Close();
            }
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
            BoutonSupprimer.Visibility = Visibility.Collapsed;

            Titre.Content = "Modification de note";

            ComboBoxConfidentialite.IsEnabled = true;
            ComboBoxCategorie.IsEnabled = true;
            ComboBoxNature.IsEnabled = true;
            TextCommentaire.IsReadOnly = false;
            DateCreationNote.IsEnabled = true;
        }

        /// <summary>
        /// Bouton Valider la modification de la note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Lucas/Nordine </author>
        private async void ClickValider(object sender, RoutedEventArgs e)
        {
            //Si la modification de la note est correct
            if (isCreateOk(this.note))
            {
                //on appel le dao pour mettre a jour la note
                NoteDAO dao = new NoteDAO();
                await dao.UpdateNote(note);
                notes.UpdateNote(note);

                //Le champs redeviennent non-editable
                BoutonValider.Visibility = Visibility.Collapsed;
                BoutonModifier.Visibility = Visibility.Visible;
                BoutonSupprimer.Visibility = Visibility.Visible;

                Titre.Content = "Note";

                ComboBoxConfidentialite.IsEnabled = false;
                ComboBoxCategorie.IsEnabled = false;
                ComboBoxNature.IsEnabled = false;
                TextCommentaire.IsReadOnly = true;
                DateCreationNote.IsEnabled = false;

            }

        }

        /// <summary>
        /// Définit si l'on peut créer la note au moment de valider
        /// </summary>
        /// <param name="note">note à créer</param>
        /// <returns>true si elle est correcte, faux sinon</returns>
        /// <author>Nordine</author>
        public bool isCreateOk(Note note)
        {
            bool valide = true;


            if (note.Confidentialite == null)
            {
                valide = false;
                PopUp popUp = new PopUp("Création", "Veuillez choisir une confidentialité", TYPEICON.ERREUR);
                popUp.ShowDialog();
            }
            else if (note.Confidentialite != CONFIDENTIALITE.MEDICAL && note.Categorie == CATEGORIE.MEDICAL)
            {
                valide = false;
                PopUp popUp = new PopUp("Création", "Une note de nature 'Medical' doit être de confidentialité 'Medical'", TYPEICON.ERREUR);
                popUp.ShowDialog();
            }
            else if(note.Categorie == null)
            {
                valide = false;

                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création note", "Veuillez choisir une catégorie", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Note creation", "Please choose a category", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
            else if (note.Nature == null)
            {
                valide = false; 

                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création note", "Veuillez choisir une nature", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Note creation", "Please choose a nature", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
            else if (note.DatePublication > DateTime.Now)
            {
                valide = false;

                if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                {
                    PopUp popUp = new PopUp("Création note", "Veuillez choisir une date correcte", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Note creation", "Please choose a correct date", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }

            return valide;
        }


        /// <summary>
        /// Quand on change la combobox des confidentialité change la confidentialite de la note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxConfidentialite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxConfidentialite.SelectedIndex)
            {
                case 0:
                    note.Confidentialite = CONFIDENTIALITE.MEDICAL;
                    break;
                case 1:
                    note.Confidentialite = CONFIDENTIALITE.CONFIDENTIEL;
                    break;
                case 2:
                    note.Confidentialite = CONFIDENTIALITE.INTERNE;
                    break;
                case 3:
                    note.Confidentialite = CONFIDENTIALITE.PUBLIC;
                    break;

            }

        }

        /// <summary>
        /// Quand on change la categorie de la combobox, change l'attribut note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxCategorie.SelectedIndex)
            {
                case 0:
                    note.Categorie = CATEGORIE.ABSENTEISME;
                    break;
                case 1:
                    note.Categorie = CATEGORIE.PERSONNEL;
                    break;
                case 2:
                    note.Categorie = CATEGORIE.MEDICAL;
                    break;
                case 3:
                    note.Categorie = CATEGORIE.RESULTATS;
                    break;
                case 4:
                    note.Categorie = CATEGORIE.ORIENTATION;
                    break;
                case 5:
                    note.Categorie = CATEGORIE.AUTRE;
                    break;
            }

        }

        /// <summary>
        /// Quand on change la Nature de la combobox, change l'attribut note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ComboBoxNature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxNature.SelectedIndex)
            {
                case 0:
                    note.Nature = NATURE.MAIL;
                    break;
                case 1:
                    note.Nature = NATURE.RDV;
                    break;
                case 2:
                    note.Nature = NATURE.LETTRE;
                    break;
                case 3:
                    note.Nature = NATURE.APPEL;
                    break;
                case 4:
                    note.Nature = NATURE.AUTRE;
                    break;
            }
        }

        /// <summary>
        /// quand on change la date, change aussi l'attribut note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateCreationNote.SelectedDate != null)
                note.DatePublication = (DateTime)DateCreationNote.SelectedDate;
        }

        /// <summary>
        /// Quand on change le commentaire, on change aussi l'attribut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void TextCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            note.Commentaire = (string)TextCommentaire.Text;
        }


        /// <summary>
        /// Supprime la note via le DAO et ferme la fenetre
        /// </summary>
        /// <author>Nordine</author>
        private void ClickSupprimer(object sender, RoutedEventArgs e)
        {
            NoteDAO dao = new NoteDAO();
            dao.DeleteNote(this.note);
            notes.RemoveNote(this.note);

            this.Close();
        }


    }
}
