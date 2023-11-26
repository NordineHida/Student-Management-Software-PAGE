﻿using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;

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
        /// <param name="annee">annee courante</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        public bool CreateUtilisateur(Utilisateur user, int annee);

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public IEnumerable<Utilisateur> GetAllUtilisateurs();

        /// <summary>
        /// Regarde si l'utilisateur avec le logon et le mdp envoyé en paramètres existe, si oui, le renvoie
        /// </summary>
        /// <param name="login">login de l'utilisateur dont on vérifie l'existence</param>
        /// <param name="mdp">mdp (hashé) de l'utilisateur dont on vérifie l'existence</param>
        /// <returns>l'utilisateur, s'il existe</returns>
        public Utilisateur? GetUtilisateurByLoginMDP(string login, string mdp);

        /// <summary>
        /// Modifie le rôle d'un utilisateur
        /// </summary>
        /// <param name="user">utilisateur dont le rôle va être changé</param>
        /// <param name="role">Nouveau rôle attribué</param>
        /// <param name="annee">annee pour laquelle le role donne est actif</param>
        /// <returns>vrai si le changement a été effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public bool UpdateRole(Utilisateur user, ROLE role, int annee);
    }
}
