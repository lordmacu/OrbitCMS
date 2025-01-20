using Core.Entities;

namespace Core.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> AddAsync(Post post);
        Task<bool> SlugExistsAsync(string slug);

    }
}
