using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    /// <summary>
    /// Controlleur de l'API pour les notes
    /// </summary>
    /// <author>Laszlo</author>
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

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à Supprimer</param>
        /// <author>Laszlo</author>
        [HttpPost("DeleteNote")]
        public ActionResult DeleteNote([FromBody] Note note)
        {
            ActionResult reponse = BadRequest();
            //Si la note n'est pas null
            if (note != null)
            {
                //si la suppression de la note a été un succès on renvoie OK
                if (NoteManager.Instance.DeleteNote(note)) reponse = Ok();
            }
            return reponse;
        }

        /// <summary>
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">Note à Supprimer</param>
        /// <author>Laszlo</author>
        [HttpGet("GetAllNotesByApogee")]
        public ActionResult<IEnumerable<Etudiant>> GetAllNotesByApogee(int apogeeEtudiant)
        {
            //Cas par défaut
            ActionResult<IEnumerable<Etudiant>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            IEnumerable<Note> notes = NoteManager.Instance.GetAllNotesByApogee(apogeeEtudiant);

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (notes != null) reponse = Ok(notes);
            return reponse;
        }

        [HttpGet("GetAllNotes")]
        public ActionResult<IEnumerable<Etudiant>> GetAllNotes()
        {
            //Cas par défaut
            ActionResult<IEnumerable<Etudiant>> reponse = BadRequest();

            //On récupere les etudiants depuis le manager
            IEnumerable<Note> notes = NoteManager.Instance.GetAllNotes();

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (notes != null) reponse = Ok(notes);
            return reponse;
        }


    }
}
