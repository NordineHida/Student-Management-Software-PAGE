using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    public interface IUtilisateurDAO
    {
        /// <summary>
        /// Ajoute un utilisateur à la BDD
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public Task CreateUtilisateur(Utilisateur user);

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public Task<IEnumerable<Utilisateur>> GetAllUtilisateurs();
    }
}
