using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model.StockageSQLite
{
    public class NoteConfidentiel
    {
        [PrimaryKey, AutoIncrement]
        public int IdNote { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }

    }
}
