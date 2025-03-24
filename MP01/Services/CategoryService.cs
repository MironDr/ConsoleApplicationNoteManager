using MP01.Context;
using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;

public class CategoryService
{
    private readonly AppDbContext _context;
    private int _currentMaxId;

    public CategoryService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _currentMaxId = _context.GetAllOfType<CategoryModel>().Any() ? _context.GetAllOfType<CategoryModel>().Max(n => n.Id) : 0;
    }
    
    public void AddCategory(CategoryDTO categoryDTO)
    {
        CategoryModel category = new CategoryModel
        {
            CategoryName = categoryDTO.CategoryName
        };
        
         category.SetId(++_currentMaxId);
        _context.Add(category);
        
      
        
    }
    

    public CategoryModel? GetCategoryById(int? id)
    {
        if(id == null)
            return null;
        
        return _context.GetById<CategoryModel>((int)id);
    }
    
    
    public List<CategoryModel> GetCategories()
    {
        return _context.GetAllOfType<CategoryModel>();
    }
    
    
}