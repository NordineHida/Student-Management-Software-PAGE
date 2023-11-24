using PAGE.Model;
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
        /// Ajoute un ensemble d'étudiant étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task AddSeveralEtu(IEnumerable<Etudiant> listeEtu);

        /// <summary>
        /// Renvoi tout les étudiants de la BDD
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public Task<IEnumerable<Etudiant>> GetAllEtu();

        /// <summary>
        /// Ajoute un étudiant ou le modifie s'il existe deja
        /// </summary>
        /// <param name="etudiant">étudiant à ajouter</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task AddEtudiant(Etudiant etudiant);

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <returns></returns>
        /// <author>Nordine</author>
        public Task CreateEtu(Etudiant etu);


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public Task<Dictionary<Etudiant,int>> GetAllEtuByCategorie(CATEGORIE categorie);

    }
}

