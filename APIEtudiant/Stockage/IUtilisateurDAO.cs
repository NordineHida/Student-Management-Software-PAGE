using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO pour les utilisateurs
    /// </summary>
    /// <author>Laszlo</author>
    public interface IUtilisateurDAO 
    {
        /// <summary>
        /// Ajoute un utilisateur à la BDD
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        public bool CreateUtilisateur(Utilisateur user);

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public IEnumerable<Utilisateur> GetAllUtilisateurs();
    }
}
