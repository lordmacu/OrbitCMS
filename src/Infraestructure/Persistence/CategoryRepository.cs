using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a category asynchronously by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the category to retrieve.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains the found Category object,
        /// or null if no category matches the provided id.
        /// </returns>
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }


        /// <summary>
        /// Retrieves a category asynchronously by its name or slug.
        /// </summary>
        /// <param name="value">The name or slug of the category to search for.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains the found Category object,
        /// or null if no category matches the provided name or slug.
        /// </returns>
        public async Task<Category?> GetByNameOrSlugAsync(string value)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == value || c.Slug == value);
        }


        /// <summary>
        /// Adds a new category to the database asynchronously.
        /// </summary>
        /// <param name="category">The Category object to be added to the database.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

    }
}
