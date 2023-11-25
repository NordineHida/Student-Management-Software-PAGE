using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    /// <summary>
    /// Interface de DAO pour les années entre le cient et l'API
    /// </summary>
    /// <author>Yamato</author>
    interface IAnneeDAO
    {
        /// <summary>
        /// Ajoute une Année à la BDD
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <returns>true si l'ajout est effectué</returns>
        /// <author>Yamato</author>
        public Task CreateAnnee(int annee);

        /// <summary>
        /// Récupère toutes les années de la bdd
        /// </summary>
        /// <returns>liste d'années de la bdd</returns>
        /// <author>Yamato</author>
        public Task<List<Annee>> GetAllAnnee();

        /// <summary>
        /// Supprime une année de la bdd
        /// </summary>
        /// <param name="annee">année à supprimer</param>
        /// <returns>true si la suppression est effectué</returns>
        public Task DeleteAnnee(int annee);
    }
}
