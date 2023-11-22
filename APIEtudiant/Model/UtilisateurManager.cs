using APIEtudiant.Stockage;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Manager d'utilisateurs
    /// </summary>
    /// <author>Laszlo</author>
    public class UtilisateurManager
    {
        #region Singleton

        private static UtilisateurManager instance;

        /// <summary>
        /// Renvoie la seule instance de NoteManager
        /// </summary>
        /// <author>Laszlo</author>
        public static UtilisateurManager Instance
        {
            get
            {
                if (instance == null) instance = new UtilisateurManager();
                return instance;
            }
        }

        private UtilisateurManager()
        {
        }

        #endregion

        //DAO d'utilisateur
        private IUtilisateurDAO UtilisateurDAO => new UtilisateurDAOOracle();

        /// <summary>
        /// Ajoute un Utilisateur à la BDD
        /// </summary>
        /// <param name="user">Utilisateur à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public bool CreateUtilisateur(Utilisateur user)
        {
            return UtilisateurDAO.CreateUtilisateur(user);
        }
    }
}
