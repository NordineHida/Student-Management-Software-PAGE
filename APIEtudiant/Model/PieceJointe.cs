namespace APIEtudiant.Model
{
    public class PieceJointe
    {
        private int idPieceJointe;
        private IFormFile fichier;
        private string filePath;
        private int idNote;

        /// <summary>
        /// Récupère ou définit l'id de la piece jointe
        /// </summary>
        /// <author>Yamato</author>
        public int IdPieceJointe { get { return idPieceJointe; } set { idPieceJointe = value; } }

        /// <summary>
        /// Récupère ou définit le fichier de la piece jointe
        /// </summary>
        /// <author>Yamato</author>
        public IFormFile Fichier { get { return fichier; } set { fichier = value; } }

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
        /// <param name="fichier">fichier</param>
        /// <author>Yamato</author>
        public PieceJointe(IFormFile fichier, string filePath) 
        {
            this.fichier = fichier;
            this.filePath = filePath;
        }

    }
}
