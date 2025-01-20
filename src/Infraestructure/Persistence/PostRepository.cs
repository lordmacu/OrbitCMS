using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore; 
namespace Infrastructure.Persistence
{
    public class PostRepository : IPostRepository
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

        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _dbContext.Posts.AnyAsync(p => p.Slug == slug);
        }

    }
}
