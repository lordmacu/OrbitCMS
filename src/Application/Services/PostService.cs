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

        public PostService(IPostRepository postRepository, IStatusRepository statusRepository, IPostTypeRepository postTypeRepository)
        {
            _postRepository = postRepository;
            _statusRepository = statusRepository;
            _postTypeRepository = postTypeRepository;
        }

        public async Task<Guid> CreatePostAsync(CreatePostCommand command)
        {

         var statusId = command.StatusId 
                ?? await _statusRepository.GetIdByNameAsync("Draft");
         var postTypeId = command.PostType 
                ?? await _postTypeRepository.GetIdByNameAsync("post");

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
                PostTypeId = postTypeId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPost = await _postRepository.AddAsync(post);
            return createdPost.Id;
        }
    }
}
 