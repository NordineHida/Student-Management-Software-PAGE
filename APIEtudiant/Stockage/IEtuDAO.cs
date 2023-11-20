using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO des étudiants
    /// </summary>
    /// <author>Nordine</author>
    public interface IEtuDAO
    {
        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu();

        /// <summary>
        /// Ajoute un étudiant
        /// </summary>
        /// <param name="etudiant">étudiant à ajouter</param>
        /// <returns>true si l'ajout a fonctionner</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etudiant);


        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu);

    }
}
