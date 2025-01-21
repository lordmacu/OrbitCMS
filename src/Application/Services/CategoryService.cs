using Application.Common;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Processes a list of category identifiers or names, retrieving existing categories or creating new ones as needed.
        /// </summary>
        /// <param name="categories">A list of strings representing category identifiers (GUIDs) or names.</param>
        /// <returns>
        /// A list of Category objects corresponding to the input categories. 
        /// For each input:
        /// - If it's a valid GUID and exists, the corresponding Category is returned.
        /// - If it's a valid GUID but doesn't exist, a default Category is returned.
        /// - If it's not a GUID, it's treated as a name/slug:
        ///   - If it exists, the corresponding Category is returned.
        ///   - If it doesn't exist, a new Category is created and returned.
        /// </returns>
        public async Task<List<Category>> ProcessCategoriesAsync(List<string> categories)
        {
            var categoryModels = new List<Category>();

            foreach (var category in categories)
            {
                if (Guid.TryParse(category, out var categoryId))
                {
                    var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                    if (existingCategory != null)
                    {
                        categoryModels.Add(existingCategory);
                    }
                    else
                    {
                        categoryModels.Add(await GetOrCreateDefaultCategoryAsync());
                    }
                }
                else
                {
                    var existingCategory = await _categoryRepository.GetByNameOrSlugAsync(category);
                    if (existingCategory != null)
                    {
                        categoryModels.Add(existingCategory);
                    }
                    else
                    {
                        var newCategory = new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = category,
                            Slug = ContentHelper.GenerateSlug(category)
                        };
                        await _categoryRepository.AddAsync(newCategory);
                        categoryModels.Add(newCategory);
                    }
                }
            }

            return categoryModels;
        }


        /// <summary>
        /// Retrieves or creates a default category.
        /// </summary>
        /// <remarks>
        /// This method first attempts to retrieve a category with the name or slug "default".
        /// If such a category doesn't exist, it creates a new category named "Default".
        /// </remarks>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
        /// The task result contains the retrieved or newly created default <see cref="Category"/>.
        /// </returns>
        private async Task<Category> GetOrCreateDefaultCategoryAsync()
        {
            var defaultCategory = await _categoryRepository.GetByNameOrSlugAsync("default");
            if (defaultCategory != null)
            {
                return defaultCategory;
            }

            defaultCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Default",
                Slug = ContentHelper.GenerateSlug("Default")
            };
            await _categoryRepository.AddAsync(defaultCategory);

            return defaultCategory;
        }

    }
}
