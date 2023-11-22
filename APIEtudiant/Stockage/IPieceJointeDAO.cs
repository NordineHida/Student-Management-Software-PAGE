using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    public interface IPieceJointeDAO
    {
        public bool SaveFilepath(PieceJointe pieceJointe);
    }
}
