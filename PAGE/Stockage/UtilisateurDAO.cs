
using DocumentFormat.OpenXml.Spreadsheet;
using PAGE.Model;
using PAGE.Model.Enumerations;
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
    public class UtilisateurDAO : IUtilisateurDAO
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
                string apiUrl = "https://localhost:7038/Utilisateur/CreateUtilisateur";

                // Convertissez l'Utilisateur en JSON
                string userSerialise = JsonSerializer.Serialize(user);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(userSerialise, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Utilisateur", "L'utilisateur est créé", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("User", "User has been created", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Token?> Connexion(string login, string mdp)
        {
            Token token = null;
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = String.Format("https://localhost:7038/Utilisateur/Connexion?login={0}&mdp={1}",login,mdp);

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant le token
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On le deserialise
                token = JsonSerializer.Deserialize<Token>(reponseString);

                if (token.UserToken == null)
                {
                    PopUp popUp = new PopUp("Connexion", "Il y a eu un problème lors de la connexion. Veuillez Réessayer", TYPEICON.ERREUR);
                    popUp.ShowDialog();
                }
                else
                {
                    PopUp popUp = new PopUp("Connexion", "Vous êtes connecté sur l'application.", TYPEICON.SUCCES);
                    popUp.ShowDialog();
                }
            }
            return token;
        }
    }
}
