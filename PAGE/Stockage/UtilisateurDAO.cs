
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
    public class UtilisateurDAO
    {
        /// <summary>
        /// Ajoute un utilisateur à la BDD
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        public async Task CreateUtilisateur(Utilisateur user)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Note/CreateNote";

                // Convertissez la note en JSON
                string noteSerialise = JsonSerializer.Serialize(user);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(noteSerialise, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    PopUp popUp = new PopUp("Note", "La note est crée", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
