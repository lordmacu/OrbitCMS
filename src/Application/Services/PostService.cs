using Application.Features.Posts.Commands;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Guid> CreatePostAsync(CreatePostCommand command)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Content = command.Content,
                StatusId = command.StatusId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPost = await _postRepository.AddAsync(post);
            return createdPost.Id;
        }
    }
}
 