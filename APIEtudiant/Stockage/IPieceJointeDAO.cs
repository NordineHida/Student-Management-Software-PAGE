using APIEtudiant.Model;

namespace APIEtudiant.Stockage
{
    /// <summary>
    /// Interface de DAO pour les piece jointes
    /// </summary>
    /// <author>Yamato</author>
    public interface IPieceJointeDAO
    {
        /// <summary>
        /// Ajoute une piece jointe à la BDD
        /// </summary>
        /// <param name="pieceJointe">Piece jointe à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Yamato</author>
        public bool CreatePieceJointe(PieceJointe pieceJointe);


    }
}
