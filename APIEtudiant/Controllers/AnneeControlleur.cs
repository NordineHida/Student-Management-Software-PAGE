using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    /// <summary>
    /// Controlleur de l'API pour les les années
    /// </summary>
    /// <author>Yamato</author>
    [ApiController]
    [Route("[controller]")]
    public class AnneeControlleur : ControllerBase
    {
        /// <summary>
        /// Ajoute une année à la BDD
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <author>Yamato</author>
        [HttpPost("CreateAnnee")]
        public ActionResult CreateAnnee([FromBody] Annee annee)
        {
            ActionResult reponse = BadRequest();
            //Si l'utilisateur n'est pas null
            if (annee != null)
            {
                //si l'ajout de l'utilisateur a été un succès on renvoie OK
                if (AnneeManager.Instance.CreateAnnee(annee)) reponse = Ok();
            }
            return reponse;
        }
    }
}
