using MP01.DTOs;
using MP01.Models;
using MP01.Services;

namespace MP01.Factories;


public class TextNoteFactory : NoteFactory
{
    public override NoteModel? CreateNote(NoteDTO noteDTO)
    {
        var textNoteDTO = (TextNoteDTO)noteDTO;
        TextNoteModel? newNote = new TextNoteModel
        {
            Title = textNoteDTO.Title,
            Description = textNoteDTO.Description,
            Content = textNoteDTO.Content
        };
        return newNote;
        
    }

    
}