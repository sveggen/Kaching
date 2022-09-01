using Kaching.Data;
using Kaching.Models;

namespace Kaching.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;
    
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }   
    
    public List<Category> GetCategories()
    {
        return _context.Category.ToList();
    }
}