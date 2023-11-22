using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    /// <summary>
    /// Interface de DAO pour les piece jointes
    /// </summary>
    public interface IPieceJointeDAO
    {
        /// <summary>
        /// Ajoute une piece jointe à la BDD
        /// </summary>
        /// <param name="pieceJointe">Piece jointe à ajouter</param>
        /// <returns>true si l'ajout est un succès</returns>
        /// <author>Yamato</author>
        public Task UploadFile(PieceJointe pieceJointe);
    }
}
