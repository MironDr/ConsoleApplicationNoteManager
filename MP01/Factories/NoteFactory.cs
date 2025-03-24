using MP01.DTOs;
using MP01.Models;
using MP01.Services;

namespace MP01.Factories;


public abstract class NoteFactory
{
    public abstract NoteModel? CreateNote(NoteDTO noteDTO);
    
}

