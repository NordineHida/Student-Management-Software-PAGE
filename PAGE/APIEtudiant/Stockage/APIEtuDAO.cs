using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using System.Text.Json;
using System.Threading.Tasks;

namespace PAGE.APIEtudiant.Stockage
{
    /// <summary>
    /// Implémentation du DAO de communication avec l'API
    /// </summary>
    public class APIEtuDAO : IAPIDAO
    {
        public async Task AddSeveralEtu(IEnumerable<Etudiant> listeEtu)
        {
            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/EtuControlleur/AddSeveralEtu";

                    // Convertissez la liste d'étudiants en JSON
                    string listeEtudiantSerialise = JsonSerializer.Serialize(listeEtu);

                    // Créez le contenu de la requête POST
                    HttpContent content = new StringContent(listeEtudiantSerialise, Encoding.UTF8, "application/json");

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("L'ajout des étudiants a réussi !");
                    }
                    else 
                    {
                        Console.WriteLine("L'ajout des étudiants a échoué. Code de réponse : " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'appel de l'API : " + ex.Message);
            }
        }
    }
}
