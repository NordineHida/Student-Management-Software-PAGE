using APIEtudiant.Stockage;

namespace APIEtudiant.Model
{
    public class PieceJointeManager
    {
        #region Singleton

        private static PieceJointeManager instance;

        /// <summary>
        /// Renvoie la seule instance de PieceJointeManager
        /// </summary>
        /// <author>Yamato</author>
        public static PieceJointeManager Instance
        {
            get
            {
                if (instance == null) instance = new PieceJointeManager();
                return instance;
            }
        }

        private PieceJointeManager()
        {
        }

        #endregion

        //DAO d'étudiant 
        private IEtuDAO EtuDAO => EtudiantDAOOracle.Instance;

        /// <summary>
        /// Ajoute une note à la BDD
        /// </summary>
        /// <param name="pieceJointe">Piece jointe à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Yamato</author>
        public bool CreatePieceJointe(PieceJointe pieceJointe)
        {
            return EtuDAO.CreatePieceJointe(pieceJointe);
        }

        
    }
}
