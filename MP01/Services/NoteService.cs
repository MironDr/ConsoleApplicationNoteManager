using MP01.Context;
using MP01.DTOs;
using MP01.Factories;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;



public class NoteService
{
    private readonly AppDbContext _context;
    private int _currentMaxId;

    public NoteService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _currentMaxId = _context.GetAllNotesBase().Any() ? _context.GetAllNotesBase().Max(n => n.Id) : 0;
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
    
    
    public List<NoteModel?> GetNotes()
    {
        return _context.GetAllNotesBase();
    }
    
    
}