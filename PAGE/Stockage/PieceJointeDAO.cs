using PAGE.Model;
using PAGE.Vue.Ecran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    public class PieceJointeDAO : IPieceJointeDAO
    {
        /// <summary>
        /// Crée une piece jointe et l'ajoute à la BDD
        /// </summary>
        /// <param name="pieceJointe">piece jointe crée</param>
        /// <returns>la tache qu'est d'ajouter la piece jointe à la BDD</returns>
        /// <author>Yamato</author>
        public async Task CreatePieceJointe(PieceJointe pieceJointe)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Note/CreatePieceJointe";

                // Convertissez la note en JSON
                string pieceJointeSerialise = JsonSerializer.Serialize(pieceJointe);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(pieceJointeSerialise, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    PopUp popUp = new PopUp("Piece Jointe", "La pièce jointe est ajouté", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
