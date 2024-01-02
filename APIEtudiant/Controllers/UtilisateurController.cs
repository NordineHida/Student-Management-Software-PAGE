using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;
using APIEtudiant.Stockage;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    /// <summary>
    /// Controlleur de l'API pour les utilisateurs
    /// </summary>
    /// <author>Laszlo</author>
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : ControllerBase
    {
        /// <summary>
        /// Ajoute un Utilisateur à la BDD
        /// </summary>
        /// <param name="user">Utilisateur à ajouter</param>
        /// <param name="annee">annee courante</param>
        /// <author>Laszlo</author>
        [HttpPost("CreateUtilisateur")]
        public ActionResult CreateUtilisateur([FromBody] Utilisateur user, int annee)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            if (user != null)
            {
                //si l'ajout de l'utilisateur a été un succès on renvoie OK
                if (UtilisateurManager.Instance.CreateUtilisateur(user, annee)) reponse = Ok();
            }
            return reponse;
        }

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        [HttpGet("GetAllUtilisateurs")]
        public ActionResult<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            //Cas par défaut
            ActionResult<IEnumerable<Utilisateur>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            IEnumerable<Utilisateur> users = UtilisateurManager.Instance.GetAllUtilisateurs();

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (users != null) reponse = Ok(users);
            return reponse;
        }

        /// <summary>
        /// Connecte un utilisateur sur l'application
        /// </summary>
        ///<param name="login">login de l'utilisateur</param>
        ///<param name="mdp">mot de passe de l'utiisateur</param>
        /// <returns>Le token prouvant la connexion</returns>
        [HttpGet("Connexion")]
        public ActionResult<Token?> Connexion(string login, string mdp)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            Token token = null;
            Utilisateur ? user = UtilisateurManager.Instance.GetUtilisateurByLoginMDP(login, mdp);
            if (user != null)
            {
                token = UtilisateurManager.Instance.CreateToken(user);
            }
            if (token != null) reponse = Ok(token);
            return reponse;
       }

        /// <summary>
        /// Modifie le role d'un utilisateur
        /// </summary>
        /// <param name="user">Utilisateur dont le rôle doit être modifié</param>
        /// <param name="role">nouveau role attribué</param>
        /// <param name="annee">annee pour laquelle le role donne est actif</param>
        /// <author>Laszlo</author>
        [HttpPost("UpdateRole")]
        public ActionResult UpdateRole([FromBody] Utilisateur user,ROLE role, int annee)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            if (user != null)
            {
                //si l'ajout de l'utilisateur a été un succès on renvoie OK
                if (UtilisateurManager.Instance.UpdateRole(user, role,annee)) reponse = Ok();
            }
            return reponse;
        }
    }
}
