using System.Text;
using MP01.DTOs;
using MP01.Factories;
using MP01.Models;
using MP01.Services;
using MP01.Utilities;

namespace MP01.View;

public class NotesView
{

    private readonly NoteService _noteService;
    private readonly CategoryService _categoryService;

    private readonly HashSet<Type> _noteTypes = new();
    
    public NotesView()
    {
        _noteService = ServiceLocator.Get<NoteService>();
        _categoryService = ServiceLocator.Get<CategoryService>();
    }
    
    public void CreateCategoryFromView()
    {
        Console.WriteLine("<<<<<<CATEGORY CREATION>>>>>>");
        Console.WriteLine("Enter the category name:");
        string categoryName = Console.ReadLine() ?? string.Empty;
        
        CategoryDTO categoryDTO = new CategoryDTO
        {
            CategoryName = categoryName
        };
        
        _categoryService.AddCategory(categoryDTO);
    }

    private NoteDTO CreateNoteFromView()
    {
        Console.WriteLine("<<<<<<NOTE CREATION>>>>>>");
        
        Console.WriteLine("Enter the title of the note:");
        string title = Console.ReadLine() ?? string.Empty;
        
        Console.WriteLine("Enter the description of the note:");
        string description = Console.ReadLine() ?? string.Empty;

        NoteDTO noteDTO = new NoteDTO
        {
            Title = title,
            Description = description
        };
        return noteDTO;
    }
    
    public void CreateTextNoteFromView()
    {
        NoteDTO noteDTO = CreateNoteFromView();

        List<string> contentList = new List<string>();
        Console.WriteLine("Enter the content of the note (leave empty to finish):");

        while (true)
        {
            string content = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(content))
                break; 

            contentList.Add(content);
        }

        TextNoteDTO textNoteDto = new TextNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
            Content = contentList
        };

        _noteService.AddNote(textNoteDto);
    }

    public void CompleteNote(NoteModel note)
    {
        int? id = GetCategoryFromView()?.Id;

        if (id == null)
        {
            Console.WriteLine("Category not found");
            return;
        }

        note.SetCategoryId(id);
        _noteService.UpdateNote(note);
    }

    private CategoryModel? GetCategoryFromView()
    {
        List<CategoryModel> categories = _categoryService.GetCategories();
        
        if(categories.Count == 0)
            return null;
        
        Console.WriteLine("<<<<<<CATEGORIES>>>>>>");
        
        for(int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i+1}. {categories[i].CategoryName}");
        }
        
        Console.WriteLine("Choose a category:");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out var index))
        {
            if(index >= 1 && index <= categories.Count)
                return categories[index-1];
            
        }
        
        return null;
        
    }
    
    public NoteModel? GetNoteFromView()
    {
        
        List<NoteModel?> notes = _noteService.GetAllNotes();
        
        if(notes.Count == 0)
            return null;
        
        Console.WriteLine("<<<<<<NOTES>>>>>>");

        
        for(int i = 0; i < notes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {NoteToStringList((NoteModel)notes[i])}\n");
        }
        
        Console.WriteLine("Choose a note:");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out var index))
        {
            if(index >= 1 && index <= notes.Count)
                return notes[index-1];
            
        }
        
        return null;
        
        
    }
    
    public void ViewNote(NoteModel note)
    {

        Console.WriteLine(NoteToString(note));
        
    }


    private string NoteToStringList(NoteModel note)
    {
        return NoteToStringListImpl(note);
    }
    
    private string NoteToStringListImpl(NoteModel? note)
    {
        return $"Tittle: {note.Title}, Description: {note.Description}, CreatedAt: {note.CreatedAt}";
    }
    
    
    
    private string NoteToString(dynamic note)
    {
        return NoteToStringImpl(note);
    }
    
    private string NoteToStringImpl(NoteModel? note)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append($"<<<<<<{note.Title}>>>>>>\n");
        
        stringBuilder.Append($"Id: {note.Id}\n");
        
        stringBuilder.Append($"Description: {note.Description}\n");
        
        var category = _categoryService.GetCategoryById(note.CategoryId);
        if (category != null)
        {
            stringBuilder.Append($"Category: {category.CategoryName}\n");
        }
        
        return stringBuilder.ToString();
    }
    
    private string NoteToStringImpl(TextNoteModel? note)
    {
        StringBuilder stringBuilder = new StringBuilder(NoteToStringImpl((NoteModel)note));
        
        stringBuilder.Append("<<<<<<Text Blocks>>>>>>\n");
        
        foreach (string str in note.Content)
        {
            stringBuilder.Append("---------------------\n");
            stringBuilder.Append($"{str}\n");
        }
    
        
        return stringBuilder.ToString();
    }
    
    
}