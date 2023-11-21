using PAGE.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    /// <summary>
    /// Interface de DAO pour les notes
    /// </summary>
    public interface INoteDAO
    {

        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public Task CreateNote(Note note);

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo</author>
        public Task DeleteNote(Note note);

        /// <summary>
        /// Renvoi toutes les notes d'un étudiant
        /// </summary>
        /// <returns>Un ensemble de notest</returns>
        /// <author>Laszlo</author>
        public Task<IEnumerable<Note>> GetAllNotesByApogee(int apogeeEtudiant);


        /// <summary>
        /// Modifie une note à la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine</author>
        public Task UpdateNote(Note note);
    }
}
