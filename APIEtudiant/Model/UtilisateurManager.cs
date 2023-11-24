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

        private ITokenDAO TokenDAO => new TokenDAOOracle();

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

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        { 
            return UtilisateurDAO.GetAllUtilisateurs();
        }

        /// <summary>
        /// Regarde si l'utilisateur avec le login et le mdp envoyé en paramètres existe, si oui, le renvoie
        /// </summary>
        /// <param name="login">login de l'utilisateur dont on vérifie l'existence</param>
        /// <param name="mdp">mdp (hashé) de l'utilisateur dont on vérifie l'existence</param>
        /// <returns>l'utilisateur, s'il existe</returns>
        public Utilisateur GetUtilisateurByLoginMDP(string login, string mdp)
        {
            return UtilisateurDAO.GetUtilisateurByLoginMDP(login, mdp);
        }


        /// <summary>
        /// Crée un Token pour un utilisateur
        /// </summary>
        /// <param name="utilisateur">Utilisateur que le token vise</param>
        /// <returns>le token créé</returns>
        public Token CreateToken(Utilisateur utilisateur)
        {
            return TokenDAO.CreateToken(utilisateur);
        }
    }
}
