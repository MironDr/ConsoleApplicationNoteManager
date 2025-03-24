using SQLite;

namespace MP01.Models;

public abstract class BaseModel
{
    [PrimaryKey]
    public int Id { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }
}