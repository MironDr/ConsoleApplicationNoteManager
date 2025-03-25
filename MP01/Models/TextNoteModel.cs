using System.Text.Json;
using SQLite;

namespace MP01.Models;

public class TextNoteModel: NoteModel
{
    public string? ContentJson { get; set; }
    
    [Ignore] 
    public List<string>? Content
    {
        get => string.IsNullOrEmpty(ContentJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(ContentJson);
        set => ContentJson = JsonSerializer.Serialize(value);
    }
    
}