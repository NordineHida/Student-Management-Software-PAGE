namespace APIEtudiant.Model
{
    public class PieceJointe
    {
        private int idPieceJointe;
        private string filePath;
        private int idNote;

        public int IdPieceJointe { get { return idPieceJointe; } set { idPieceJointe = value; } }
        public string FilePath { get { return filePath; } set { filePath = value; } }
        public int IdNote { get { return idNote; } set { idNote = value; } }


        public PieceJointe(string filePath, int idNote) 
        {
            this.filePath = filePath;
            this.idNote = idNote;
        }

    }
}
