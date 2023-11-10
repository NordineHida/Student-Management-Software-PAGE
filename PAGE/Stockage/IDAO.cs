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
    /// <author>Nordine</author>
    public interface IDAO
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
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="note">Note à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Laszlo</author>
        public Task CreateNote(Note note);

        /// <summary>
        /// Ajoute une piece jointe à la bdd
        /// </summary>
        /// <param name="pj"></param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Yamato</author>
        public Task CreatePieceJointe(PieceJointe pj);
    }
}

