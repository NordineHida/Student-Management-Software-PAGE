using PAGE.Model.Enumerations;
using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PAGE.Stockage
{
    public interface IUtilisateurDAO
    {
        /// <summary>
        /// Ajoute un utilisateur à la BDD
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <param name="annee">annee courante</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public Task CreateUtilisateur(Utilisateur user, int annee);

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>Une liste d'utilisateurs</returns>
        /// <author>Laszlo</author>
        public Task<IEnumerable<Utilisateur>> GetAllUtilisateurs();

        /// <summary>
        /// Connecte un utilisateur sur l'application
        /// </summary>
        /// <returns></returns>
        public Task<Token?> Connexion(string login,string mdp);

        /// <summary>
        /// Modifie le rôle d'un utilisateur pour l'année courante
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public Task UpdateRole(Utilisateur user, ROLE role, int annee);
    }
}
