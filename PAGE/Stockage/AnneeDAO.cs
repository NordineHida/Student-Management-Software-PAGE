using PAGE.Model;
using PAGE.Vue.Ecran;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task CreateAnnee(int annee)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/AnneeControlleur/CreateAnnee?annee={annee}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);


                if (reponse.IsSuccessStatusCode)
                {
                    PopUp popUp = new PopUp("Année", "L'année est ajouté", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
