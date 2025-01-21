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
