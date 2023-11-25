using PAGE.Model;
using PAGE.Vue.Ecran;
using System.Collections.Generic;
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
                else
                {
                    PopUp popUp = new PopUp("Erreur année", "L'année existe déjà", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Renvoi toutes les années
        /// </summary>
        /// <returns>Un ensemble d'années</returns>
        /// <author>Yamato</author>
        public async Task<List<Annee>> GetAllAnnee()
        {
            // Liste d'années
            List<Annee> annee = new List<Annee>();

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/AnneeControlleur/GetAllAnnee";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste d'années
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Etudiant>
                annee = JsonSerializer.Deserialize<List<Annee>>(reponseString);
            }
            return annee;
        }

        /// <summary>
        /// Supprime une année de la bdd
        /// </summary>
        /// <param name="annee">année supprimé</param>
        /// <returns>la tache qu'est de supprimer une année de la BDD</returns>
        /// <author>Yamato</author>
        public async Task DeleteAnnee(int annee)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/AnneeControlleur/DeleteAnnee?annee={annee}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);


                if (reponse.IsSuccessStatusCode)
                {
                    PopUp popUp = new PopUp("Année", "L'année est supprimé", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }
    }
}
