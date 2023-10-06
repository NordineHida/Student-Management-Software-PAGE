using APIEtudiant.Stockage;
using DocumentFormat.OpenXml.Office2010.Excel;
using PAGE.Model;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class APIEtuDAO : IAPIDAO
    {

        #region Singleton
        private static APIEtuDAO instance;

        /// <summary>
        /// Seul instance de DAO d'étudiant 
        /// </summary>
        public static APIEtuDAO Instance
        {
            get
            {
                if (instance == null) instance = new APIEtuDAO();
                return instance;
            }
        }

        private APIEtuDAO()
        {

        }
        #endregion

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
                MessageBox.Show("Erreur lors de l'appel de l'API (GetAllEtu): " + ex.Message, "Erreur avec l'API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return etudiants;
        }
    }
}
