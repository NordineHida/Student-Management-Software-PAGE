using APIEtudiant.Model;
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
        [HttpPost("CreateNote")]
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
    }
}
