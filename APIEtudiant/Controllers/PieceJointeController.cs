using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIEtudiant.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PieceJointeController : ControllerBase
    {
        [HttpPost("upload")]
        public ActionResult UploadFile([FromForm] PieceJointe pieceJointe)
        {
            ActionResult reponse = BadRequest();
            try
            {
                // Validate if the file is present
                if (pieceJointe.Fichier == null || pieceJointe.Fichier.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Generate a unique file name or use the original file name
                var fileName = Guid.NewGuid().ToString() + "_" + pieceJointe.Fichier.FileName;

                // Specify the path to save the file
                var filePath = Path.Combine("path/to/save/", fileName);

                // Save the file to the file system
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    pieceJointe.Fichier.CopyTo(stream);
                }

                PieceJointeManager.Instance.CreatePieceJointe(pieceJointe);

                reponse = Ok("File uploaded successfully");
            }
            catch (Exception ex)
            {
                reponse = StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return reponse;
        }
    }
}
