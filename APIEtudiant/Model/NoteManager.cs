using APIEtudiant.Stockage;

namespace APIEtudiant.Model
{
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

        //DAO d'étudiant 
        private IEtuDAO EtuDAO => EtudiantDAOOracle.Instance;

        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateNote(Note note)
        {
            return EtuDAO.CreateNote(note);
        }

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à supprimer</param>
        /// <returns>true si la suppression est un succès</returns>
        /// <author>Laszlo</author>
        public bool DeleteNote(Note note)
        {
            return EtuDAO.DeleteNote(note);
        }

        /// <summary>
        /// Renvoie toutes les notes d'un étudiant 
        /// </summary>
        /// <returns>la liste de notes/returns>
        /// <author>Laszlo</author>
        public IEnumerable<Note> GetAllNotesByApogee(int apogeeEtudiant)
        {
            return EtuDAO.GetAllNotesByApogee(apogeeEtudiant);
        }
    }
}
