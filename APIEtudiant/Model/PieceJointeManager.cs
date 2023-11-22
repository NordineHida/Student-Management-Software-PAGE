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

        private IPieceJointeDAO PieceJointeDAO => new PieceJointeDAOOracle();

        public bool SavePathfile(PieceJointe pieceJointe)
        {
            return PieceJointeDAO.SaveFilepath(pieceJointe);
        }
    }
}
