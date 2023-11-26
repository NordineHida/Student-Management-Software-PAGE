using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO des étudiants
    /// </summary>
    /// <author>Nordine</author>
    public interface IEtuDAO
    {
        /// <summary>
        /// Renvoi tout les étudiants de la promo
        /// </summary>
        /// <param name="promo">Promo dont on veut le etudiants</param>
        /// <returns>la liste des étudiants de la promo</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu(Promotion promo);

        /// <summary>
        /// Ajoute un nouvelle étudiant (ou le modifie s'il existe déjà)
        /// </summary>
        /// <param name="etu">etudiant qu'on veut ajouter</param>
        /// <param name="promotion">promo dans laquel on doit mettre l'etudiant</param>
        /// <returns>si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etu, Promotion promotion);


        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <param name="promotion">promo dans laquel on ajoute les étudiants</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu, Promotion promotion);


        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS et renvoi true, sinon renvoi false
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <param name="promo">Promo actuel où on veut ajouter l'étu</param>
        /// <returns>si l'ajout est un succès</returns>
        public bool CreateEtu(Etudiant etu, Promotion promo);

        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public List<Tuple<Etudiant, int>> GetAllEtuByCategorie(CATEGORIE categorie);
    }
}
