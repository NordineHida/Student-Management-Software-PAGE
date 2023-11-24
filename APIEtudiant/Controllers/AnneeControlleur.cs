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
        [HttpGet("CreateAnnee")]
        public ActionResult CreateAnnee(int? annee)
        {
            ActionResult reponse = BadRequest("L'année existe déjà");
            //Si l'année n'est pas null
            if (annee != null)
            {
                //si l'ajout de l'année a été un succès on renvoie OK
                if (AnneeManager.Instance.CreateAnnee((int)annee)) reponse = Ok();
            }
            return reponse;
        }
    }
}
