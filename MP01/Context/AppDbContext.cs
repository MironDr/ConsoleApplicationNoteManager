using MP01.Models;
using SQLite;

namespace MP01.Context;

public class AppDbContext
{
    private readonly SQLiteConnection _connection;
    private readonly List<Type> _noteTypes = new();
    public AppDbContext(string dbPath)
    {
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<CategoryModel>();
        
        RegisterNoteTypes();
        CreateTables();
    }
    
    private void RegisterNoteTypes()
    {
        _noteTypes.Add(typeof(TextNoteModel));
        
    }
    
    private void CreateTables()
    {
        foreach (var noteType in _noteTypes)
        {
            _connection.CreateTable(noteType);
        }
    }

 
    public List<NoteModel?> GetAllNotesBase()
    {
        var notes = new List<NoteModel?>();

        foreach (var noteType in _noteTypes)
        {
            var method = typeof(AppDbContext).GetMethod(nameof(GetAllOfType))?.MakeGenericMethod(noteType);
        
            if (method != null)
            {
                
                var result = method.Invoke(this, null);

                if (result is IEnumerable<NoteModel> noteList)
                {

                    notes.AddRange(noteList.Cast<NoteModel?>());

                }
            }
        }

        return notes;
    }
    public void Add<T>(T entity) where T : new()
    {
        _connection.Insert(entity);
    }
    
    public List<T> GetAllOfType<T>() where T : new()
    {
        return _connection.Table<T>().ToList();
    }

    
    public void Update<T>(T entity) where T : new()
    {
        
        _connection.Update(entity);
       
    }
    

    public T? GetById<T>(int id) where T : new()
    {
        return _connection.Find<T>(id);
    }

    
 
}