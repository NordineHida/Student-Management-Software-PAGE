﻿using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Http;
using PAGE.Model;
using PAGE.Stockage;
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
        private Notes notes;

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
                        MessageBox.Show("Succès !", "Succès", MessageBoxButton.OK);
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
                        MessageBox.Show("Succès !", "Succès", MessageBoxButton.OK);
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
            //Dictionnaire d'étudiant (cle = num apogee, valeur = etudiant)
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
        /// Crée une note et l'ajoute à la BDD
        /// </summary>
        /// <param name="note">note crée</param>
        /// <returns>la tache qu'est d'ajouter la note à la BDD</returns>
        /// <author>Laszlo</author>
        public async Task CreateNote(Note note)
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
                    MessageBox.Show("Succès !", "Succès", MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Renvoie les notes d'un etudiant
        /// </summary>
        /// <returns>Un ensemble den notes</returns>
        /// <author>Laszlo & Nordine</author>
        public async Task<IEnumerable<Note>> GetAllNotesByApogee(int apogeeEtudiant)
        {
            //Liste de notes 
            List<Note> notes = new List<Note>();

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/Note/GetAllNotesByApogee?apogeeEtudiant={apogeeEtudiant}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste de notes
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Note>
                notes = JsonSerializer.Deserialize<List<Note>>(reponseString);
            }

            return notes;
        }

        /// <summary>
        /// Renvoie les notes d'un etudiant
        /// </summary>
        /// <returns>Un ensemble den notes</returns>
        /// <author>Laszlo & Nordine</author>
        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            //Liste de notes 
            List<Note> notes = new List<Note>();

            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Note/GetAllNotes";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste de notes
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Note>
                notes = JsonSerializer.Deserialize<List<Note>>(reponseString);
            }

            return notes;
        }

        /// <summary>
        /// Crée une note et l'ajoute à la BDD
        /// </summary>
        /// <param name="note">note crée</param>
        /// <returns>la tache qu'est d'ajouter la note à la BDD</returns>
        /// <author>Laszlo</author>
        public async Task DeleteNote(Note note)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Note/DeleteNote";

                // Convertissez la note en JSON
                string noteSerialise = JsonSerializer.Serialize(note);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(noteSerialise, Encoding.UTF8, "application/json");

                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                 
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("La suppression est un succès", "Succès de la suppression", MessageBoxButton.OK);
                }
            }
        }
    }
}
                                                          