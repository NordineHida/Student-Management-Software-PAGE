using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO pour les notes
    /// </summary>
    /// <author>Nordine</author>
    public interface INoteDAO
    {

        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateNote(Note note);

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo</author>
        public bool DeleteNote(Note note);

        /// <summary>
        /// Renvoie toutes les notes d'un étudiant 
        /// </summary>
        /// <returns>la liste de notes/returns>
        /// <author>Laszlo</author>
        public IEnumerable<Note> GetAllNotesByApogee(int apogeeEtudiant);


        /// <summary>
        /// Modifie une note à la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine</author>
        public bool UpdateNote(Note note);

    }
}
