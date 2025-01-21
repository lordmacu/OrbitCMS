using Application.Common;
using Application.Features.Posts.Commands;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryService _categoryService;
        private readonly ISlugService _slugService;


        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="postRepository">The repository for managing posts.</param>
        /// <param name="statusRepository">The repository for managing post statuses.</param>
        /// <param name="postTypeRepository">The repository for managing post types.</param>
        /// <param name="categoryRepository">The repository for managing categories.</param>
        /// <param name="categoryService">The service for processing categories.</param>
        /// <param name="slugService">The service for generating and managing slugs.</param>
        /// <param name="userRepository">The repository for managing users.</param>
        public PostService(
            IPostRepository postRepository,
            IStatusRepository statusRepository,
            IPostTypeRepository postTypeRepository,
            ICategoryRepository categoryRepository,
             ICategoryService categoryService,
             ISlugService slugService,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _statusRepository = statusRepository;
            _postTypeRepository = postTypeRepository;
            _userRepository = userRepository;
            _categoryService = categoryService;
            _slugService = slugService;
        }


        /// <summary>
        /// Retrieves a paginated list of posts asynchronously.
        /// </summary>
        /// <param name="pageNumber">The number of the page to retrieve (1-based index).</param>
        /// <param name="pageSize">The number of posts to include per page.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains a 
        /// <see cref="PagedResult{PostDto}"/> with the requested posts, total count, and pagination information.
        /// </returns>
        public async Task<PagedResult<PostDto>> GetPaginatedPostAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var totalPosts = await _postRepository.CountAsync();

            var posts = await _postRepository.GetPaginatedPostAsync(skip, pageSize);

            return new PagedResult<PostDto>
            {
                Items = posts,
                Total = totalPosts,
                NextPage = (pageNumber * pageSize < totalPosts) ? pageNumber + 1 : (int?)null,
                PreviousPage = (pageNumber > 1) ? pageNumber - 1 : (int?)null
            };
        }

        /// <summary>
        /// Retrieves a post by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the post to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the 
        /// <see cref="PostDto"/> representing the retrieved post.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// Thrown when a post with the specified ID is not found in the repository.
        /// </exception>
        public async Task<PostDto> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new DllNotFoundException($"Post with ID {id} not found.");
            }
            return post;
        }

        /// <summary>
        /// Creates a new post asynchronously based on the provided command.
        /// </summary>
        /// <param name="command">The command containing the details for creating the post.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The task result contains
        /// the <see cref="Guid"/> of the newly created post.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the default user 'Default Admin' does not exist in the database.
        /// </exception>
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

            var uniqueSlug = await _slugService.GenerateUniqueSlugAsync(command.Title ?? string.Empty, (ISlugRepository)_postRepository);

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

    }
}
