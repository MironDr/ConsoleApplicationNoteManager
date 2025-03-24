



namespace MP01.Models;

public class NoteModel : BaseModel
{


    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public int? CategoryId { get; set; }


    public void SetCategoryId(int? categoryId)
    {
        CategoryId = categoryId;
    }
    
    
}
    
