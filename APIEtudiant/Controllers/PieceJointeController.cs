using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    public class PieceJointeController : ControllerBase
    {
        /// <summary>
        /// Controlleur de l'API pour les piece jointes
        /// </summary>
        /// <author>Laszlo</author>
        [ApiController]
        [Route("[controller]")]
        public class NoteController : ControllerBase
        {
            /// <summary>
            /// Ajoute une piece jointe à la BDD
            /// </summary>
            /// <param name="pieceJointe">Piece jointe à ajouter</param>
            /// <author>Yamato</author>
            [HttpPost("CreatePieceJointe")]
            public ActionResult CreatePieceJointe([FromBody] PieceJointe pieceJointe)
            {
                ActionResult reponse = BadRequest();
                //Si la note n'est pas null
                if (pieceJointe != null)
                {
                    //si l'ajout de la note a été un succès on renvoie OK
                    if (PieceJointeManager.Instance.CreatePieceJointe(pieceJointe)) reponse = Ok();
                }
                return reponse;
            }
        }
    }
}
