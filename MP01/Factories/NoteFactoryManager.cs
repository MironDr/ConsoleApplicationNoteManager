using MP01.DTOs;
using MP01.Models;

namespace MP01.Factories;

public class NoteFactoryManager
{
    private readonly Dictionary<Type, NoteFactory> _factories = new();

    public NoteFactoryManager()
    {
        _factories[typeof(TextNoteDTO)] = new TextNoteFactory();
    }

    public NoteModel? CreateNoteByFactory(NoteDTO noteDTO)
    {
        if (_factories.TryGetValue(noteDTO.GetType(), out var factory))
        {
            return factory.CreateNote(noteDTO);
        }

        throw new NotSupportedException($"Factory for {noteDTO.GetType()} not found.");
    }
}