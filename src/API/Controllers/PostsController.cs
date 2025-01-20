using Application.Services;
using Application.Features.Posts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        {
            var postId = await _postService.CreatePostAsync(command);
            return CreatedAtAction(nameof(Create), new { id = postId }, postId);
        }
    }
}
