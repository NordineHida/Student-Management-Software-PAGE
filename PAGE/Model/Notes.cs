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
        /// <author>Nordine</author>
        public Notes(List<Note> listeNote)
        {
            listeNotes = listeNote;
        }

        /// <summary>
        /// Ajoute une note à la liste
        /// </summary>
        /// <param name="note">note à ajouter</param>
        /// <author>Laszlo</author>
        public void AddNote(Note note)
        {
            listeNotes.Add(note);
            Notifier("note ajoutee");
        }

        /// <summary>
        /// Ajoute une note à la liste
        /// </summary>
        /// <param name="note">note à ajouter</param>
        /// <author>Laszlo</author>
        public void RemoveNote(Note note)
        {
            listeNotes.Remove(note);
            Notifier("note supprimee");
        }
    }
}