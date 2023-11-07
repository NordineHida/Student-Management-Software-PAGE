using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using PAGE.Model;

namespace APIEtudiant.Controllers
{
    /// <summary>
    /// Controlleur de l'API pour les notes
    /// </summary>
    /// <author>Nordine</author>
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <author>Laszlo</author>
        [HttpPost("CreateNote")]
        public ActionResult CreateNote([FromBody] Note note)
        {
            ActionResult reponse = BadRequest();
            //Si la note n'est pas null
            if (note != null)
            {
                //si l'ajout de la note a été un succès on renvoie OK
                if (NoteManager.Instance.CreateNote(note)) reponse = Ok();
            }
            return reponse;
        }
    }
}
