using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using APIEtudiant.Stockage;

namespace APIEtudiant.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PieceJointeController : ControllerBase
    {
        private readonly PieceJointeManager pieceJointeManager; // Injectez la dépendance si nécessaire

        public PieceJointeController(PieceJointeManager pieceJointeManager)
        {
            this.pieceJointeManager = pieceJointeManager;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] PieceJointe pieceJointe)
        {
            ActionResult reponse = BadRequest("Aucun fichier n'a été téléversé");

            try
            {
                if (pieceJointe == null) reponse = BadRequest();

                // Générez un chemin unique pour le fichier
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pieceJointe.File.FileName);
                string filePath = Path.Combine("chemin/vers/dossier/upload", fileName);

                // Enregistrez le fichier sur le disque
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pieceJointe.File.CopyToAsync(stream);
                }

                // Stockez le chemin du fichier dans la base de données Oracle
                pieceJointeManager.SavePathfile(pieceJointe);

                reponse = Ok("Fichier téléversé avec succès.");
            }
            catch (Exception ex)
            {
                // Gérez les erreurs
                reponse = StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
            return reponse;
        }

        
    }    
}
