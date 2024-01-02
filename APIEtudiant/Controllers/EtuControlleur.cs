using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using APIEtudiant.Model.Enumerations;

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
        /// Renvoi un IEnumerable d'étudiant avec tout les étudiants de la BDD de la promo choisi
        /// </summary>
        /// <param name="anneeDebut">Annee de la promotion</param>
        /// <param name="typeBUT">0 = But1, 1 = but2, 2 = but3</param>
        /// <returns>IEnumerable d'étudiant</returns>
        /// <author>Nordine</author>
        [HttpGet("GetAllEtu")]
        public ActionResult<IEnumerable<Etudiant>> GetAllEtu(int anneeDebut, int typeBUT)
        {
            //Cas par défaut
            ActionResult<IEnumerable<Etudiant>> reponse = BadRequest();

            NOMPROMOTION nompromo = NOMPROMOTION.BUT1; 
            switch (typeBUT)
            {
                case 1:
                    nompromo = NOMPROMOTION.BUT2;
                    break;
                case 2:
                    nompromo = NOMPROMOTION.BUT3;
                    break;

            }

            Promotion promo = new Promotion(nompromo, anneeDebut);
            //On récupere les etudiants depuis le manager
            IEnumerable<Etudiant> etudiants = EtuManager.Instance.GetAllEtu(promo);

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
        public ActionResult AddEtu([FromBody] Tuple<Etudiant,Promotion> etudiantEtPromo )
        {
            ActionResult reponse = BadRequest();
            if (etudiantEtPromo.Item1 != null && etudiantEtPromo.Item2 != null)
            {
                if (EtuManager.Instance.AddEtu(etudiantEtPromo.Item1, etudiantEtPromo.Item2)) reponse = Ok();
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
        public ActionResult CreateEtu([FromBody] Tuple<Etudiant, Promotion> etudiantEtPromo)
        {
            ActionResult reponse = BadRequest("Etudiant existe déjà");
            if (etudiantEtPromo.Item1 != null && etudiantEtPromo.Item2 != null)
            {
                if (EtuManager.Instance.CreateEtu(etudiantEtPromo.Item1, etudiantEtPromo.Item2)) reponse = Ok();
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
        public ActionResult AddSeveralEtu([FromBody] Tuple<IEnumerable<Etudiant>,Promotion> listeEtuEtPromo)
        {
            ActionResult reponse = BadRequest();

            //Si le chemin est vide on renvoi un message d'erreur
            if (listeEtuEtPromo.Item1 == null) reponse = BadRequest("Il n'y as pas d'étudiants");

            //Sinon si l'ajout est un succes alors on renvoi Ok
            else if (EtuManager.Instance.AddSeveralEtu(listeEtuEtPromo.Item1, listeEtuEtPromo.Item2)) reponse = Ok();

            return reponse;
        }


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <param name="anneeDebut">Annee de la promotion voulu</param>
        /// <param name="categorie">Categorie choisie</param>
        /// <param name="nomPromo">nompromo de la promo voulu</param>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        [HttpGet("GetAllEtuByCategorie")]
        public ActionResult<List<Tuple<Etudiant, int>>> GetAllEtuByCategorie(CATEGORIE categorie, NOMPROMOTION nomPromo,int anneeDebut)
        {
            Promotion promo = new Promotion(nomPromo, anneeDebut);

            //Cas par défaut
            ActionResult<List<Tuple<Etudiant, int>>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            List<Tuple<Etudiant, int>> etudiants = EtuManager.Instance.GetAllEtuByCategorie(categorie, promo);

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (etudiants != null) reponse = Ok(etudiants);
            return reponse;
        }
    }
}
