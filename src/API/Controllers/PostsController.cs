using Application.Services;
using Application.Features.Posts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/posts")]
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

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var posts = await _postService.GetAllAsync(pageNumber, pageSize);
            return Ok(posts);
        }
    }
}
