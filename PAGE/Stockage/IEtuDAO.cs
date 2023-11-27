using PAGE.Model;
using PAGE.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{

    /// <summary>
    /// Interface de DAO pour les etudiant entre le client et l'API 
    /// </summary>
    /// <author>Nordine</author>
    public interface IEtuDAO
    {

        /// <summary>
        /// Ajoute plusieurs etudiants à la BDD
        /// </summary>
        /// <param name="listeEtu">liste d'étudiant a ajouter</param>
        /// <param name="promo">La promotion dans laquel on doit ajouter les etus</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task AddSeveralEtu(IEnumerable<Etudiant> listeEtu, Promotion promo);

        /// <summary>
        /// Renvoi tout les étudiants de la BDD de cette promotion
        /// </summary>
        /// <param name="promo">promotion dont on veut les étudiants</param>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public Task<IEnumerable<Etudiant>> GetAllEtu(Promotion promo);

        /// <summary>
        /// Ajoute un étudiant à la BDD ou le modifie s'il existe déjà
        /// </summary>
        /// <param name="etudiant">Etudiant à ajouté</param>
        /// <param name="promo">promo dans laquel on doit ajouter</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task AddEtudiant(Etudiant etudiant, Promotion promo);

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS
        /// </summary>
        /// <param name="etudiant">Etudiant à ajouté</param>
        /// <param name="promo">promo dans laquel on doit ajouter</param>
        /// <returns></returns>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task CreateEtu(Etudiant etudiant, Promotion promo);


        /// <summary>
        /// Renvoi tout les étudiants de la promotion qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public Task<List<Tuple<Etudiant, int>>> GetAllEtuByCategorie(CATEGORIE categorie, Promotion promo);

    }
}

