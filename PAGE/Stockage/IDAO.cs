using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{

    /// <summary>
    /// Interface de DAO entre le client et l'API
    /// </summary>
    public interface IDAO
    {

        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns></returns>
        public Task AddSeveralEtu(IEnumerable<Etudiant> listeEtu);

        /// <summary>
        /// Renvoi tout les étudiants de la BDD
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        public Task<IEnumerable<Etudiant>> GetAllEtu();
    }
}

