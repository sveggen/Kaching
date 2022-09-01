using Kaching.Models;

namespace Kaching.Repositories;

public interface ICategoryRepository
{
    public List<Category> GetCategories();
}