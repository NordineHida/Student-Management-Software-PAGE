using Microsoft.AspNetCore.Http;
using PAGE.Model;
using PAGE.Vue.Ecran;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    public class PieceJointeDAO : IPieceJointeDAO
    {
        /// <summary>
        /// Téléverse une pièce jointe vers l'API.
        /// </summary>
        /// <param name="file">Fichier à téléverser.</param>
        /// <returns>La tâche qui représente le téléversement de la pièce jointe.</returns>
        /// <author>Yamato</author>
        public async Task UploadFile(PieceJointe pieceJointe)
        {
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/PieceJointe/UploadFile";

                // Créez le contenu de la requête POST
                using (var content = new MultipartFormDataContent())
                {
                    // Ajoutez le fichier à la requête
                    content.Add(new StreamContent(pieceJointe.File.OpenReadStream())
                    {
                        Headers =
                            {
                                ContentLength = pieceJointe.File.Length,
                                ContentType = new MediaTypeHeaderValue(pieceJointe.File.ContentType)
                            }
                    }, "file", pieceJointe.File.FileName);

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        PopUp popUp = new PopUp("Piece Jointe", "La piece jointe est ajouté", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                }
            }
        }
    }
}
