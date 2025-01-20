using Application.Features.Posts.Commands;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IStatusRepository _statusRepository;

        public PostService(IPostRepository postRepository, IStatusRepository statusRepository)
        {
            _postRepository = postRepository;
            _statusRepository = statusRepository;
        }

        public async Task<Guid> CreatePostAsync(CreatePostCommand command)
        {

         var statusId = command.StatusId 
                ?? await _statusRepository.GetIdByNameAsync("Draft");

            if (statusId == null)
            {
                throw new InvalidOperationException("Default status 'Draft' does not exist in the database.");
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Content = command.Content,
                StatusId = statusId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPost = await _postRepository.AddAsync(post);
            return createdPost.Id;
        }
    }
}
 