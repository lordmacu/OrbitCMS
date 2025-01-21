using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence
{
    public class PostRepository : IPostRepository, ISlugRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Posts.CountAsync();
        }

        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _dbContext.Posts.AnyAsync(p => p.Slug == slug);
        }

        public async Task<List<PostDto>> GetAllAsync(int skip, int take)
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
                    Author = new AuthorDto
                    {
                        Id = p.Author.Id,
                        Name = p.Author.Name
                    },
                    Categories = p.Categories.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Slug = c.Slug
                    }).ToList(),
                    PostType = new PostTypeDto
                    {
                        Id = p.PostType.Id,
                        Name = p.PostType.Name
                    },
                    Status = new StatusDto
                    {
                        Id = p.Status.Id,
                        Name = p.Status.Name
                    },
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }
    }
}
