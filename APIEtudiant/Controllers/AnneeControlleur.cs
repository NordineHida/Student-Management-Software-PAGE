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

        /// <summary>
        /// Renvoi un IEnumerable d'années avec toutes les années de la BDD
        /// </summary>
        /// <returns>IEnumerable d'années</returns>
        /// <author>Yamato</author>
        [HttpGet("GetAllAnnee")]
        public ActionResult<List<Annee>> GetAllEtu()
        {
            //Cas par défaut
            ActionResult<List<Annee>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            List<Annee> annee = AnneeManager.Instance.GetAllAnnee();

            //Si c'est pas null on renvoi un Ok avec les années
            if (annee != null) reponse = Ok(annee);
            return reponse;
        }

        /// <summary>
        /// Supprime une année de la BDD
        /// </summary>
        /// <param name="annee">Année à Supprimer</param>
        /// <author>Yamato</author>
        [HttpGet("DeleteAnnee")]
        public ActionResult DeleteAnnee(int annee)
        {
            ActionResult reponse = BadRequest();
            //Si l'année n'est pas null
            if (annee != null)
            {
                //si la suppression de l'année a été un succès on renvoie OK
                if (AnneeManager.Instance.DeleteAnnee(annee)) reponse = Ok();
            }
            return reponse;
        }
    }
}
