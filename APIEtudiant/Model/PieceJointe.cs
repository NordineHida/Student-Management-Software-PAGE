namespace APIEtudiant.Model
{
    public class PieceJointe
    {
        private int idPieceJointe;
        private string fileName;
        private string filePath;
        private int idNote;
        private Note note;

        public int IdPieceJointe { get { return idPieceJointe; } set { value = idPieceJointe; } }
        public string FileName { get { return fileName; } set { value = fileName; } }
        public string FilePath { get { return filePath; } set { value = filePath; } }
        public int IdNote { get { return idNote; } set { value = idNote; } }
        public Note Note { get { return note; } set { value = note; } }


        public PieceJointe(string fileName, string filePath, int idNote) 
        {
            this.fileName = fileName;
            this.filePath = filePath;
            this.idNote = idNote;
        }

    }
}
