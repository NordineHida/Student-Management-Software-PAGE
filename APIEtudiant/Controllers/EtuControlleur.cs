using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using APIEtudiant.Model.Enumerations;
using System.Collections.Generic;

namespace APIEtudiant.Controllers
{
    /// <summary>
    /// Controlleur de l'API
    /// </summary>
    /// <author>Nordine</author>
    [ApiController]
    [Route("[controller]")]
    public class EtuControlleur : ControllerBase
    {
        /// <summary>
        /// Renvoi un IEnumerable d'étudiant avec tout les étudiants de la BDD
        /// </summary>
        /// <returns>IEnumerable d'étudiant</returns>
        /// <author>Nordine</author>
        [HttpGet("GetAllEtu")]
        public ActionResult<IEnumerable<Etudiant>> GetAllEtu()
        {
            //Cas par défaut
            ActionResult<IEnumerable<Etudiant>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            IEnumerable<Etudiant> etudiants = EtuManager.Instance.GetAllEtu();

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (etudiants != null) reponse = Ok(etudiants);
            return reponse;
        }


        /// <summary>
        /// Ajoute un etudiant à la BDD ou le modifie s'il existe déjà
        /// </summary>
        /// <param name="etu">etudiant à ajouter</param>
        /// <returns>si l'ajout à fonctionné</returns>
        /// <author>Nordine</author>
        [HttpPost("AddEtu")]
        public ActionResult AddEtu([FromBody] Etudiant? etu)
        {
            ActionResult reponse = BadRequest();
            if (etu != null)
            {
                if (EtuManager.Instance.AddEtu(etu)) reponse = Ok();
            }

            return reponse;
        }

        /// <summary>
        /// Ajoute un etudiant à la BDD seulement s'il n'existe PAS
        /// </summary>
        /// <param name="etu">etudiant à ajouter</param>
        /// <returns>si l'ajout à fonctionné</returns>
        /// <author>Nordine</author>
        [HttpPost("CreateEtu")]
        public ActionResult CreateEtu([FromBody] Etudiant? etu)
        {
            ActionResult reponse = BadRequest("Etudiant existe déjà");
            if (etu != null)
            {
                if (EtuManager.Instance.CreateEtu(etu)) reponse = Ok();
            }

            return reponse;
        }


        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        [HttpPost("AddSeveralEtu")]
        public ActionResult AddSeveralEtu([FromBody] IEnumerable<Etudiant> listeEtu)
        {
            ActionResult reponse = BadRequest();

            //Si le chemin est vide on renvoi un message d'erreur
            if (listeEtu == null) reponse = BadRequest("Il n'y as pas de d'étudiants");

            //Sinon si l'ajout est un succes alors on renvoi Ok
            else if (EtuManager.Instance.AddSeveralEtu(listeEtu)) reponse = Ok();

            return reponse;
        }


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        [HttpGet("GetAllEtuByCategorie")]
        public ActionResult<List<Tuple<Etudiant, int>>> GetAllEtuByCategorie(CATEGORIE categorie)
        {
            //Cas par défaut
            ActionResult<List<Tuple<Etudiant, int>>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            List<Tuple<Etudiant, int>> etudiants = EtuManager.Instance.GetAllEtuByCategorie(categorie);

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (etudiants != null) reponse = Ok(etudiants);
            return reponse;
        }
    }
}
