using Core.Entities;

namespace Core.Interfaces
{
public interface ICategoryService
    {
        Task<List<Category>> ProcessCategoriesAsync(List<string> categories);
    }
}
