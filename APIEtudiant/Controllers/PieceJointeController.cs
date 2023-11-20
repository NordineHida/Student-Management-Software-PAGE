using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PieceJointeController : ControllerBase
    {
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
