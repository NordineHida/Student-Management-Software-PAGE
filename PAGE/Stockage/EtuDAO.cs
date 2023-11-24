using PAGE.Model;
using PAGE.Vue.Ecran;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE.Stockage
{
    /// <summary>
    /// Implémentation du DAO de communication avec l'API
    /// </summary>
    /// <author>Nordine</author>
    public class EtuDAO : IEtuDAO
    {
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
                        if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                        {
                            PopUp popUp = new PopUp("Importation", "Les étudiants sont ajoutés", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        }
                        else 
                        {
                            PopUp popUp = new PopUp("Import", "Students are added", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Ajoute un étudiant à la BDD
        /// </summary>
        /// <param name="etudiant">Etudiant à ajouté</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public async Task AddEtudiant(Etudiant etudiant)
        {
            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/EtuControlleur/AddEtu";

                    // Convertissez l'étudiants en JSON
                    string etudiantSerialise = JsonSerializer.Serialize(etudiant);

                    // Créez le contenu de la requête POST
                    HttpContent content = new StringContent(etudiantSerialise, Encoding.UTF8, "application/json");

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                        {
                            PopUp popUp = new PopUp("Ajout d'étudiant", "L'étudiant à bien été ajouté", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        }
                        else
                        {
                            PopUp popUp = new PopUp("Add student", "The student has been added", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public async Task<IEnumerable<Etudiant>> GetAllEtu()    
        {
            //Liste d'étudiant
            List<Etudiant> etudiants = new List<Etudiant>();

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
            return etudiants;
        }

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public async Task CreateEtu(Etudiant etudiant)
        {
            try
            {
                // Créez une instance de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Spécifiez l'URL de l'API
                    string apiUrl = "https://localhost:7038/EtuControlleur/CreateEtu";

                    // Convertissez l'étudiants en JSON
                    string etudiantSerialise = JsonSerializer.Serialize(etudiant);

                    // Créez le contenu de la requête POST
                    HttpContent content = new StringContent(etudiantSerialise, Encoding.UTF8, "application/json");

                    // Effectuez la requête POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {
                        if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                        {
                            PopUp popUp = new PopUp("Ajout d'étudiant", "L'étudiant à bien été crée", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        }
                        else
                        {
                            PopUp popUp = new PopUp("Add student", "The student has been created", TYPEICON.SUCCES);
                            popUp.ShowDialog();
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                        {
                            PopUp popUp = new PopUp("Ajout d'étudiant", "Le numéro apogée existe déjà", TYPEICON.ERREUR);
                            popUp.ShowDialog();
                        }
                        else
                        {
                            PopUp popUp = new PopUp("Add student", "The apogee number already exists", TYPEICON.ERREUR);
                            popUp.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public async Task<Dictionary<Etudiant, int>> GetAllEtuByCategorie(CATEGORIE categorie)
        {
            //Dictionnaire d'étudiant (cle = etudiant , valeur = etudiant)
            Dictionary<Etudiant,int> etudiantEtNbNote = new Dictionary<Etudiant, int>();

            int idCategorie=-1;

            switch (categorie)
            {
                case CATEGORIE.ABSENTEISME:
                    idCategorie = 0;
                    break;
                case CATEGORIE.PERSONNEL:
                    idCategorie = 1;
                    break;
                case CATEGORIE.MEDICAL:
                    idCategorie = 2;
                    break;
                case CATEGORIE.RESULTATS:
                    idCategorie = 3;
                    break;
                case CATEGORIE.ORIENTATION:
                    idCategorie = 4;
                    break;
                case CATEGORIE.AUTRE:
                    idCategorie = 5;
                    break;
            }

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/EtuControlleur/GetAllEtuByCategorie?categorie={idCategorie}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste d'étudiant
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit le dico etudiant/nbnote
                etudiantEtNbNote = JsonSerializer.Deserialize<Dictionary<Etudiant, int>>(reponseString);
            }

            return etudiantEtNbNote;
        }

    }
}
                                                          