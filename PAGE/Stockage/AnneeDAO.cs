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
    public class AnneeDAO : IAnneeDAO
    {
        /// <summary>
        /// Ajoute une Année à la BDD
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <returns>vrai si l'ajout est effectué</returns>
        /// <author>Yamato</author>
        public async Task CreateAnnee(Annee annee)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/AnneeControlleur/CreateAnnee";

                // Convertissez l'Utilisateur en JSON
                string anneeSerialise = JsonSerializer.Serialize(annee);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(anneeSerialise, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    PopUp popUp = new PopUp("Année", "L'année est créé", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
