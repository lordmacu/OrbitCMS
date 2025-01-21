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

        /// <summary>
        /// Creates a new post based on the provided command.
        /// </summary>
        /// <param name="command">The command containing the details for creating a new post.</param>
        /// <returns>
        /// An IActionResult with a CreatedAtAction result containing the newly created post's ID.
        /// The response has a 201 (Created) status code and includes the location of the newly created resource.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        {
            var postId = await _postService.CreatePostAsync(command);
            return CreatedAtAction(nameof(Create), new { id = postId }, postId);
        }


        /// <summary>
        /// Retrieves a paginated list of posts.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve. Defaults to 1 if not specified.</param>
        /// <param name="pageSize">The number of posts per page. Defaults to 10 if not specified.</param>
        /// <returns>
        /// An IActionResult containing the paginated list of posts.
        /// Returns a 200 (OK) status code with the posts data.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var posts = await _postService.GetPaginatedPostAsync(pageNumber, pageSize);
            return Ok(posts);
        }

        /// <summary>
        /// Retrieves a specific post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the post to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the requested post.
        /// Returns a 200 (OK) status code with the post data if found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id){
            var posts = await _postService.GetPostByIdAsync(id);
            return Ok(posts);
        }


    }
}
