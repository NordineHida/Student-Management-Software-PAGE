﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Controlleurs
{
    [ApiController]
    [Route("[controller]")]
    public class EtuControlleur : ControllerBase
    {
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

        [HttpPost("AddEtu")]
        public ActionResult AddEtu([FromBody] Etudiant? etu)
        {
            ActionResult reponse = BadRequest();

            if (EtuManager.Instance.AddEtu(etu)) reponse = Ok();

            return reponse;
        }


        /// <summary>
        /// Lit le fichier excel des étudiants et les ajoute
        /// </summary>
        /// <param name="pathExcel">chemin du fichier excel </param>
        /// <returns>true si l'ajout est un succes</returns>
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



    }
}
