using PAGE.Model.PatternObserveur;
using System.Collections.Generic;

namespace PAGE.Model
{
    /// <summary>
    /// Classe répertoriant toutes les notes
    /// </summary>
    /// <author>Laszlo</author>
    public class Notes : Observable
    {
        private List<Note> listeNotes;

        /// <summary>
        /// Propriété notifiant quand on set sa valeur
        /// </summary>
        /// <author>Laszlo</author>
        public List<Note> ListeNotes 
        {  
            get 
            { 
                return listeNotes; 
            } 
            set 
            { 
                listeNotes = value;
                Notifier("liste modifiee");
            } 
        }

        /// <summary>
        /// Construit une liste Notes
        /// </summary>
        public Notes(List<Note> listeNote)
        {
            listeNotes = listeNote;
        }

        public void AddNote(Note note)
        {
            listeNotes.Add(note);
            Notifier("note ajoutee");
        }

        public void RemoveNote(Note note)
        {
            listeNotes.Remove(note);
            Notifier("note supprimee");
        }
    }
}