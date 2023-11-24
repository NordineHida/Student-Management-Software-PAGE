using APIEtudiant.Model;
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
        /// <author>Laszlo</author>
        [HttpPost("CreateUtilisateur")]
        public ActionResult CreateUtilisateur([FromBody] Utilisateur user)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            if (user != null)
            {
                //si l'ajout de l'utilisateur a été un succès on renvoie OK
                if (UtilisateurManager.Instance.CreateUtilisateur(user)) reponse = Ok();
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
        /// <returns></returns>
        [HttpPost("Connexion")]
        public ActionResult<Token> Connexion(string login, string mdp)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            Token token = UtilisateurManager.Instance.CreateToken(UtilisateurManager.Instance.GetUtilisateurByLoginMDP(login, mdp));
            if (token != null) reponse = Ok(token);
            return reponse;
        }
    }
}
