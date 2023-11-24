using APIEtudiant.Stockage;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Manager d'année
    /// </summary>
    /// <author>Yamato</author>
    public class AnneeManager
    {
        #region Sinleton

        private static AnneeManager instance;

        /// <summary>
        /// Renvoie la seule instance d'AnneeManager
        /// </summary>
        /// <author>Yamato</author>
        public static AnneeManager Instance
        {
            get
            {
                if (instance == null) instance = new AnneeManager();
                return instance;
            }
        }

        private AnneeManager()
        {

        }
        #endregion

        //DAO d'année
        private IAnneeDAO AnneeDAO => new AnneeDAOOracle();

        /// <summary>
        /// Ajoute une année à la BDD
        /// </summary>
        /// <param name="annee">Année à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Yamato</author>
        public bool CreateAnnee(int annee)
        {
            return AnneeDAO.CreateAnnee(annee);
        }
    }
}
