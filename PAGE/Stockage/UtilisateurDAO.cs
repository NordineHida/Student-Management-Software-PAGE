
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
                    PopUp popUp = new PopUp("Utilisateur", "L'utilisateur est créé", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Récupère les utilisateurs de la BDD
        /// </summary>
        /// <returns>vrai si l'ajout est effectué, faux sinon</returns>
        /// <author>Laszlo</author>
        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            //Dictionnaire d'étudiant (cle = num apogee, valeur = etudiant)
            List<Utilisateur> users = new List<Utilisateur>();

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Utilisateur/GetAllUtilisateurs";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste d'étudiant
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Etudiant>
                users = JsonSerializer.Deserialize<List<Utilisateur>>(reponseString);
            }
            return users;
        }
    }
}
