using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model.StockageSQLite
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<NoteConfidentiel>().Wait();
        }

        public Task<List<NoteConfidentiel>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<NoteConfidentiel>().ToListAsync();
        }

        public Task<NoteConfidentiel> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<NoteConfidentiel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(NoteConfidentiel note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(NoteConfidentiel note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
