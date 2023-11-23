using PAGE.Model;
using PAGE.Vue.Ressources;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour NoteComponent.xaml
    /// </summary>
    public partial class NoteComponent : UserControl
    {
        /// <summary>
        /// note du component
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// contructeur de note component (applique l'image en rapport avec la categorie de la note)
        /// </summary>
        /// <param name="note"> note du component</param>
        /// <author>Nordine</author>
        public NoteComponent(Note note)
        {
            InitializeComponent();
            Note = note;

            LabelDate.Text= note.DatePublication.ToString("D");
            //Affiche la bonne image
            PictoNote.Source = RessourceManager.Instance.GetImage(note.Categorie.ToString());
        }
    }
}
