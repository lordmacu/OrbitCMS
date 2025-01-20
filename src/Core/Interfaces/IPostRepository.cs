using Core.Entities;

namespace Core.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> AddAsync(Post post);
    }
}
