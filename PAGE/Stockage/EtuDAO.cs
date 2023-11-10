using DocumentFormat.OpenXml;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE.APIEtudiant.Stockage
{
    /// <summary>
    /// Implémentation du DAO de communication avec l'API
    /// </summary>
    /// <author>Nordine</author>
    public class EtuDAO : IDAO
    {

        #region Singleton
        private static EtuDAO instance;

        /// <summary>
        /// Seul instance de DAO d'étudiant 
        /// </summary>
        /// <author>Nordine</author>
        public static EtuDAO Instance
        {
            get
            {
                if (instance == null) instance = new EtuDAO();
                return instance;
            }
        }

        private EtuDAO()
        {

        }
        #endregion

        /// <summary>
        /// Ajoute plusieurs etudiants à la BDD
        /// </summary>
        /// <param name="listeEtu">liste d'étudiant a ajouter</param>
        /// <returns></returns>
        /// <author>Nordine</author>
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
                        MessageBox.Show("L'ajout est un succès", "Succès de l'importation", MessageBoxButton.OK);
                    }
                    else 
                    {
                        MessageBox.Show("L'ajout des étudiants a échoué. Code de réponse : " + response.StatusCode, "Erreur d'import", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'appel de l'API : " + ex.Message, "Erreur avec l'API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public async Task<IEnumerable<Etudiant>> GetAllEtu()
        {
            //Dictionnaire d'étudiant (cle = num apogee, valeur = etudiant)
            List<Etudiant> etudiants = new List<Etudiant>();

            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/EtuControlleur/GetAllEtu";

                    // Effectuez la requête GET
                    HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                    //On récupere le json contenant la liste d'étudiant
                    string reponseString = await reponse.Content.ReadAsStringAsync();

                    //On la deserialise et on lit en IEnumerable qu'on convertit en List<Etudiant>
                    etudiants = JsonSerializer.Deserialize<List<Etudiant>>(reponseString);
                }
            }
            catch (Exception ex)
            {
                //PEUT ETRE PAR AFFICHER (DEMANDER CLIENT)
                MessageBox.Show("Erreur lors de l'appel de l'API (GetAllEtu) DEMANDER CLIENT SI ON VEUT AFFICHER ERReur: " + ex.Message, "Erreur avec l'API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return etudiants;
        }

        /// <summary>
        /// Crée une note et l'ajoute à la BDD
        /// </summary>
        /// <param name="note">note crée</param>
        /// <returns>la tache qu'est d'ajouter la note à la BDD</returns>
        public async Task CreateNote(Note note)
        {
            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/Note/CreateNote";

                    // Convertissez la note en JSON
                    string noteSerialise = JsonSerializer.Serialize(note);

                    // Créez le contenu de la requête POST
                    HttpContent content = new StringContent(noteSerialise, Encoding.UTF8, "application/json");

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("L'ajout est un succès", "Succès de l'importation", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("L'ajout de la note a échoué. Code de réponse : " + response.StatusCode, "Erreur d'import", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'appel de l'API : " + ex.Message, "Erreur avec l'API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Crée une piece jointe et l'ajoute a la bdd
        /// </summary>
        /// <param name="pj">piece jointe crée</param>
        /// <returns>la tache qu'est d'ajouter la piece jointe à la bdd</returns>
        /// <author>Yamato</author>
        public async Task CreatePieceJointe(PieceJointe pj)
        {
            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/PieceJointe/CreatePieceJointe";

                    // Convertissez la note en JSON
                    string pjSerialise = JsonSerializer.Serialize(pj);

                    // Créez le contenu de la requête POST
                    HttpContent content = new StringContent(pjSerialise, Encoding.UTF8, "application/json");

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("L'ajout est un succès", "Succès de l'importation", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("L'ajout de la pièce jointe a échoué. Code de réponse : " + response.StatusCode, "Erreur d'import", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'appel de l'API : " + ex.Message, "Erreur avec l'API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
