using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO pour les Annee
    /// </summary>
    /// <author>Yamato</author>
    public interface IAnneeDAO
    {
        /// <summary>
        /// Ajoute une année à la bdd
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <returns>true si l'ajout est effectué</returns>
        /// <author>Yamato</author>
        public bool CreateAnnee(int? annee);

        /// <summary>
        /// Renvoi toutes les années
        /// </summary>
        /// <returns>Un ensemble d'années</returns>
        /// <author>Yamato</author>
        public List<Annee> GetAllAnnee();

        /// <summary>
        /// Supprime une année de la bdd
        /// </summary>
        /// <param name="annee">année à supprimer</param>
        /// <returns>true si la suppression est effectué</returns>
        public bool DeleteAnnee(int? annee);

    }
}
