using Application.Common;
using Application.Features.Posts.Commands;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;


        public PostService(
            IPostRepository postRepository,
            IStatusRepository statusRepository,
            IPostTypeRepository postTypeRepository,
            ICategoryRepository categoryRepository,
             ICategoryService categoryService,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _statusRepository = statusRepository;
            _postTypeRepository = postTypeRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }

        public async Task<Guid> CreatePostAsync(CreatePostCommand command)
        {
            var statusId = command.StatusId
                           ?? await _statusRepository.GetIdByNameAsync("Draft");

            var postTypeId = command.PostType
                              ?? await _postTypeRepository.GetIdByNameAsync("post");

            Guid? userId = command.UserId
                           ?? await _userRepository.GetIdByNameAsync("Default Admin");

            if (userId == null)
            {
                throw new InvalidOperationException("Default user 'Default Admin' does not exist in the database.");
            }

            var uniqueSlug = await GenerateUniqueSlugAsync(command.Title ?? string.Empty);

            var excerpt = !string.IsNullOrWhiteSpace(command.Excerpt)
                  ? command.Excerpt
                  : ContentHelper.GenerateExcerpt(command.Content ?? string.Empty);

            var processedCategories = await _categoryService.ProcessCategoriesAsync(command.Categories);

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Content = command.Content,
                StatusId = statusId,
                PostTypeId = postTypeId,
                Slug = uniqueSlug,
                AuthorId = userId.Value,
                Categories = processedCategories,
                Excerpt = excerpt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPost = await _postRepository.AddAsync(post);
            return createdPost.Id;
        }

        private async Task<string> GenerateUniqueSlugAsync(string Title)
        {
            var slug = ContentHelper.GenerateSlug(Title);

            var counter = 1;

            while (await _postRepository.SlugExistsAsync(slug))
            {
                slug = $"{slug}-{counter}";
                counter++;
            }

            return slug;
        }
    }
}
