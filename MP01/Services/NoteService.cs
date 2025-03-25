using MP01.Context;
using MP01.DTOs;
using MP01.Factories;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;



public class NoteService
{
    private readonly AppDbContext _context;
    private readonly List<Type> _noteTypes;
    private int _currentMaxId;

    public NoteService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _noteTypes = ServiceLocator.Get<NotesTypeManager>().GetNoteTypes();
        
        _currentMaxId = GetAllNotes().Any() ? GetAllNotes().Max(n => n.Id) : 0;
        Console.WriteLine($"Current Max Id: {_currentMaxId}");
    }


    public void UpdateNote(NoteModel note)
    {
        _context.Update(note);
    }

    
    public void AddNote(NoteDTO noteDTO)
    {
        var note = ServiceLocator.Get<NoteFactoryManager>().CreateNoteByFactory(noteDTO);
        


        if (note != null)
        {
            note.SetId(++_currentMaxId);
            _context.Add(note);
        }
        
       
    }
    
    
    public List<NoteModel?> GetAllNotes()
    {
        
        var notes = new List<NoteModel?>();

        foreach (var noteType in _noteTypes)
        {
            var method = typeof(NoteService).GetMethod(nameof(GetNotesByType))?.MakeGenericMethod(noteType);
        
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

    public List<NoteModel?> GetNotesByType<T>() where T : NoteModel, new()
    {
        
        return _context.GetAllOfType<T>().Cast<NoteModel?>().ToList();
    }


}