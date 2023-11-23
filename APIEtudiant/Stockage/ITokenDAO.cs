using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO pour les tokens
    /// </summary>
    public interface ITokenDAO
    {
        /// <summary>
        /// Crée un token pour l'utilisateur passé en parametre
        /// </summary>
        /// <param name="user">utilisateur possédant le token</param>
        /// <returns>vrai si le toke a été créé, faux sinon</returns>
        /// <author>Laszlo</author>
        public bool CreateToken(Utilisateur user);
    }
}
