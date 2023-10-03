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

            //On récupere les users depuis le manager
            IEnumerable<Etudiant> etudiants = EtuManager.Instance.GetAllEtu();

            //Si c'est pas null on renvoi un Ok avec les users
            if (etudiants != null) reponse = Ok(etudiants);
            return reponse;
        }
    }
}
