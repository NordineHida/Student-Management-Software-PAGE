using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    public class PieceJointe
    {
        private int idPieceJointe;
        private string fileName;
        private string filePath;
        private int idNote;

        public int IdPieceJointe { get { return idPieceJointe; } set { value = idPieceJointe; } }
        public string FileName { get { return fileName; } set { value = fileName; } }
        public string FilePath { get { return filePath; } set { value = filePath; } }
        public int IdNote { get { return idNote; } set { value = idNote; } }


        public PieceJointe(string fileName, string filePath, int idNote)
        {
            this.fileName = fileName;
            this.filePath = filePath;
            this.idNote = idNote;
        }

    }
}
