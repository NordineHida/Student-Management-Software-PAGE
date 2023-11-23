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
        public bool CreateAnnee(Annee annee);

    }
}
