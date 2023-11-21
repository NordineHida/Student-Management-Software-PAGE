using APIEtudiant.Stockage;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Manager de note
    /// </summary>
    /// <author>Laszlo</author>
    public class NoteManager
    {
        #region Singleton

        private static NoteManager instance;

        /// <summary>
        /// Renvoie la seule instance de NoteManager
        /// </summary>
        /// <author>Laszlo</author>
        public static NoteManager Instance
        {
            get
            {
                if (instance == null) instance = new NoteManager();
                return instance;
            }
        }

        private NoteManager()
        {
        }

        #endregion

        //DAO de note 
        private INoteDAO NoteDAO => new NoteDAOOracle();

        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateNote(Note note)
        {
            return NoteDAO.CreateNote(note);
        }

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo</author>
        public bool DeleteNote(Note note)
        {
            return NoteDAO.DeleteNote(note);
        }

        /// <summary>
        /// Renvoie toutes les notes d'un étudiant 
        /// </summary>
        /// <returns>la liste de notes/returns>
        /// <author>Laszlo</author>
        public IEnumerable<Note> GetAllNotesByApogee(int apogeeEtudiant)
        {
            return NoteDAO.GetAllNotesByApogee(apogeeEtudiant);
        }

        /// <summary>
        /// Modifie une note dans la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine</author>
        public bool UpdateNote(Note? note)
        {
            return NoteDAO.UpdateNote(note);
        }

    }
}
