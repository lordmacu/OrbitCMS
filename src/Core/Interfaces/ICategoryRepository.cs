using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category?> GetByNameOrSlugAsync(string value);
        Task AddAsync(Category category);
    }
}
