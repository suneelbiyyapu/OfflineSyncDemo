using OfflineSyncDemo.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfflineSyncDemo.LocalDatabase
{
    public class StudentDatabase
    {
        readonly SQLiteAsyncConnection database;

        public StudentDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Student>().Wait();
        }

        public Task<List<Student>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Student>().ToListAsync();
        }

        public Task<Student> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Student>()
                            .Where(i => i.StudentId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Student student)
        {
            if (student.StudentId != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(student);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(student);
            }
        }

        public Task<int> DeleteNoteAsync(Student student)
        {
            // Delete a note.
            return database.DeleteAsync(student);
        }
    }
}
