﻿using PAGE.Model;
using PAGE.Model.Enumerations;
using PAGE.Vue.Ecran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE.Stockage
{
    public class NoteDAO : INoteDAO
    {

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
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Création de note", "La note est crée", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Note creation", "The note is created", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Renvoie les notes d'un etudiant
        /// </summary>
        /// <returns>Un ensemble de notes</returns>
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
        /// Supprime une note de la BDD
        /// </summary>
        /// <param name="note">note supprimée</param>
        /// <returns>la tache qu'est de supprimer la note à la BDD</returns>
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
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Suppression de note", "La note est supprimée", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Note deleted", "The note is deleted", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                }
            }
        }


        /// <summary>
        /// Modifie une note à la BDD
        /// </summary>
        /// <param name="note">Note à modifier</param>
        /// <returns>true si la modification est un succès</returns>
        /// <author>Nordine</author>
        public async Task UpdateNote(Note note)
        {
            // Créez une instance de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Spécifiez l'URL de l'API
                string apiUrl = "https://localhost:7038/Note/UpdateNote";

                // Convertissez la note en JSON
                string noteSerialise = JsonSerializer.Serialize(note);

                // Créez le contenu de la requête POST
                HttpContent content = new StringContent(noteSerialise, Encoding.UTF8, "application/json");


                // Effectuez la requête POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    if (Parametre.Instance.Langue == LANGUE.FRANCAIS)
                    {
                        PopUp popUp = new PopUp("Mise à jour", "La note est mise à jour", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                    else
                    {
                        PopUp popUp = new PopUp("Update", "The note has been updated", TYPEICON.SUCCES);
                        popUp.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Renvoi un dictionnaire("Nom Prenom", liste notes) d'une promo
        /// </summary>
        /// <param name="promo">promo dont on veut les notes</param>
        /// <returns>dictionnaire("Nom Prenom", liste notes)</returns>
        /// <author>Nordine</author>
        public async Task<Dictionary<string, IEnumerable<Note>>> GetAllNotesByPromo(Promotion promo)
        {
            //Liste de notes 
            Dictionary<string, IEnumerable<Note>> notes = new Dictionary<string, IEnumerable<Note>>();

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
                // Spécifiez l'URL de l'API
                string apiUrl = $"https://localhost:7038/Note/GetAllNotesByPromo?anneeDebut={promo.AnneeDebut}&typeBUT={typepromo}";

                // Effectuez la requête GET
                HttpResponseMessage reponse = await client.GetAsync(apiUrl);

                //On récupere le json contenant la liste de notes
                string reponseString = await reponse.Content.ReadAsStringAsync();

                //On la deserialise et on lit en IEnumerable qu'on convertit en List<Note>
                notes = JsonSerializer.Deserialize<Dictionary<string, IEnumerable<Note>>>(reponseString);
            }

            return notes;
        }



    }
}
