using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Vue.Ecran;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        /// <param name="promo">La promotion dans laquel on doit ajouter les etus</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public async Task AddSeveralEtu(IEnumerable<Etudiant> listeEtu,Promotion promo)
        {

            //Couple listeetu/promo
            Tuple<IEnumerable<Etudiant>,Promotion> aEnvoye = new Tuple<IEnumerable<Etudiant>, Promotion> (listeEtu, promo);

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/EtuControlleur/AddSeveralEtu";

                // Convertissez la liste d'étudiants et la promo en JSON
                string aEnvoyeJson = JsonSerializer.Serialize(aEnvoye);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(aEnvoyeJson, Encoding.UTF8, "application/json");

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

        /// <summary>
        /// Ajoute un étudiant à la BDD ou le modifie s'il existe déjà
        /// </summary>
        /// <param name="etudiant">Etudiant à ajouté</param>
        /// <param name="promo">promo dans laquel on doit ajouter</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public async Task AddEtudiant(Etudiant etudiant,Promotion promo)
        {

            //Couple etu/promo
            Tuple<Etudiant, Promotion> aEnvoye = new Tuple<Etudiant, Promotion>(etudiant, promo);

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/EtuControlleur/AddEtu";

                // Convertissez l'étudiants en JSON
                string aEnvoyeJson = JsonSerializer.Serialize(aEnvoye);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(aEnvoyeJson, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Ajout/Modification d'étudiant", "L'étudiant à bien été ajouté/modifié", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Add/Update student", "The student has been added/updated", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                }
            }
        }


        /// <summary>
        /// Renvoi tout les étudiants de la BDD de cette promotion
        /// </summary>
        /// <param name="promo">promotion dont on veut les étudiants</param>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public async Task<IEnumerable<Etudiant>> GetAllEtu(Promotion promo)
        {
            //Liste d'étudiant
            List<Etudiant> etudiants = new List<Etudiant>();

            // On convertir le nompromo par un int (par defaut but1) (but1=0,but2=1;but3=2)
            int typepromo = 0;
            switch (promo.NomPromotion)
            {
                case NOMPROMOTION.BUT2:
                    typepromo = 1;
                    break;
                case NOMPROMOTION.BUT3:
                    typepromo = 2;
                    break;
            }

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            { 
                //  l'URL de l'API
                string apiUrl = $"https://localhost:7038/EtuControlleur/GetAllEtu?anneeDebut={promo.AnneeDebut}&typeBUT={typepromo}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste de etudiants
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Etudiant>
                etudiants = JsonSerializer.Deserialize<List<Etudiant>>(reponseString);
            }
            return etudiants;
        }

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS
        /// </summary>
        /// <param name="etudiant">Etudiant à ajouté</param>
        /// <param name="promo">promo dans laquel on doit ajouter</param>
        /// <returns></returns>
        /// <returns></returns>
        /// <author>Nordine</author>
        public async Task CreateEtu(Etudiant etudiant, Promotion promo)
        {
            //Couple etu/promo
            Tuple<Etudiant, Promotion> aEnvoye = new Tuple<Etudiant, Promotion>(etudiant, promo);

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/EtuControlleur/CreateEtu";

                // Convertissez le couple en JSON
                string aEnvoyeJson = JsonSerializer.Serialize(aEnvoye);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(aEnvoyeJson, Encoding.UTF8, "application/json");

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


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public async Task<List<Tuple<Etudiant, int>>> GetAllEtuByCategorie(CATEGORIE categorie,Promotion promo)
        {
            //Liste de couple  etudiant/nb note de la categorie choisie
            List<Tuple<Etudiant, int>> etudiantEtNbNote = new List<Tuple<Etudiant, int>>();

            int idCategorie=-1;
            int nomPromo = -1;

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

            switch(promo.NomPromotion)
            {
                case NOMPROMOTION.BUT1:
                    nomPromo = 0;
                    break;
                case NOMPROMOTION.BUT2:
                    nomPromo = 1;
                    break;
                case NOMPROMOTION.BUT3:
                    nomPromo = 2;
                    break;
            }

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/EtuControlleur/GetAllEtuByCategorie?categorie={idCategorie}&nomPromo={nomPromo}&anneeDebut={promo.AnneeDebut}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste d'étudiant
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit le dico etudiant/nbnote
                etudiantEtNbNote = JsonSerializer.Deserialize<List<Tuple<Etudiant, int>>>(reponseString);
            }

            return etudiantEtNbNote;
        }

    }
}
                                                          