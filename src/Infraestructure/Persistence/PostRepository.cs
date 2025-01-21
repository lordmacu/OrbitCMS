using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence
{
    public class PostRepository : IPostRepository, ISlugRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access operations.</param>
        /// <remarks>
        /// This constructor injects the database context dependency, allowing the repository
        /// to interact with the database using Entity Framework Core.
        /// </remarks>
        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Asynchronously adds a new post to the database.
        /// </summary>
        /// <param name="post">The Post object to be added to the database.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. The task result contains
        /// the added Post object with any database-generated values (e.g., ID) populated.
        /// </returns>
        public async Task<Post> AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }


        /// <summary>
        /// Asynchronously counts the total number of posts in the database.
        /// </summary>
        /// <returns>
        /// A Task representing the asynchronous operation. The task result contains
        /// an integer representing the total number of posts in the database.
        /// </returns>
        public async Task<int> CountAsync()
        {
            return await _dbContext.Posts.CountAsync();
        }


        /// <summary>
        /// Checks if a post with the specified slug already exists in the database.
        /// </summary>
        /// <param name="slug">The slug to check for existence.</param>
        /// <returns>
        /// A boolean value indicating whether a post with the given slug exists.
        /// Returns true if a post with the slug is found, false otherwise.
        /// </returns>
        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _dbContext.Posts.AnyAsync(p => p.Slug == slug);
        }


        /// <summary>
        /// Retrieves a paginated list of posts from the database, including related entities.
        /// </summary>
        /// <param name="skip">The number of posts to skip before starting to return results.</param>
        /// <param name="take">The maximum number of posts to return.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a list of PostDto objects
        /// representing the retrieved posts, including their associated categories, post type, status, and author information.
        /// </returns>
        public async Task<List<PostDto>> GetPaginatedPostAsync(int skip, int take)
        {
            return await _dbContext.Posts
                .Include(p => p.Categories)
                .Include(p => p.PostType)
                .Include(p => p.Status)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(take)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Excerpt = p.Excerpt,
                    Slug = p.Slug,
                    Author = p.Author != null ? new AuthorDto
                    {
                        Id = p.Author.Id,
                        Name = p.Author.Name
                    } : null,
                    Categories = p.Categories.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Slug = c.Slug
                    }).ToList(),
                    PostType = p.PostType != null ? new PostTypeDto
                    {
                        Id = p.PostType.Id,
                        Name = p.PostType.Name
                    } : null,
                    Status = p.Status != null ? new StatusDto
                    {
                        Id = p.Status.Id,
                        Name = p.Status.Name
                    } : null,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }

    }
}
