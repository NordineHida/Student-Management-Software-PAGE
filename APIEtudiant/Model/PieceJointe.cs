namespace APIEtudiant.Model
{
    public class PieceJointe
    {
        private int idPieceJointe;
        private string filePath;
        private int idNote;

        /// <summary>
        /// Récupère ou définit l'id de la piece jointe
        /// </summary>
        /// <author>Yamato</author>
        public int IdPieceJointe { get { return idPieceJointe; } set { idPieceJointe = value; } }

        /// <summary>
        /// Récupère ou définit le chemin d'accès au fichier
        /// </summary>
        /// <author>Yamato</author>
        public string FilePath { get { return filePath; } set { filePath = value; } }

        /// <summary>
        /// Récupère ou définit l'id de la note
        /// </summary>
        public int IdNote { get { return idNote; } set { idNote = value; } }

        /// <summary>
        /// Constructeur de PieceJointe
        /// </summary>
        /// <param name="filePath">chemin d'accès du fichier</param>
        /// <param name="idNote"></param>
        /// <author>Yamato</author>
        public PieceJointe(string filePath, int idNote) 
        {
            this.filePath = filePath;
            this.idNote = idNote;
        }

    }
}
