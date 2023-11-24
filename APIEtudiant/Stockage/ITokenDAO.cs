using APIEtudiant.Model;
using System;

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
        /// <returns>vrai si le token a été créé, faux sinon</returns>
        /// <author>Laszlo</author>
        public Token? CreateToken(Utilisateur user);

    }
}
