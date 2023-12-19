using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;
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
        /// renvoi toute les note d'un étudiant de la BDD
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


        /// <summary>
        /// Modifie une note dans la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <author>Nordine</author>
        [HttpPost("UpdateNote")]
        public ActionResult UpdateNote([FromBody] Note note)
        {
            ActionResult reponse = BadRequest();
            // Si la note n'est pas null
            if (note != null)
            {
                // Si la modification de la note est un succès, renvoyer OK
                if (NoteManager.Instance.UpdateNote(note)) reponse = Ok();
            }
            return reponse;
        }

        /// <summary>
        /// Renvoi un dictionnaire("Nom Prenom", liste notes) d'une promo
        /// </summary>
        /// <param name="anneeDebut">Annee de la promotion</param>
        /// <param name="typeBUT">0 = But1, 1 = but2, 2 = but3</param>
        /// <author>Nordine</author>
        [HttpGet("GetAllNotesByPromo")]
        public ActionResult<Dictionary<string, IEnumerable<Note>>> GetAllNotesByPromo(int anneeDebut, int typeBUT)
        {
            //Cas par défaut
            ActionResult<Dictionary<string, IEnumerable<Note>>> reponse = BadRequest();

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
            Dictionary<string, IEnumerable<Note>> notes = NoteManager.Instance.GetAllNotesByPromo(promo);

            //Si c'est pas null on renvoi un Ok avec les etudiants
            if (notes != null) reponse = Ok(notes);
            return reponse;
        }
    }
}
