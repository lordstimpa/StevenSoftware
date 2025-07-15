using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StevenSoftware.Server.Models.Dto;
using StevenSoftware.Server.Service;
using System.Security.Claims;

namespace StevenSoftware.Server.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet("getblogpost/{id}")]
        public async Task<IActionResult> GetBlogPost(int id, CancellationToken cancellationToken)
        {
            var result = await _blogService.GetBlogPostById(id, cancellationToken);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getblogposts")]
        public  async Task<IActionResult> GetBlogPosts(int pageNumber, CancellationToken cancellationToken)
        {
            var result = await _blogService.GetBlogPosts(pageNumber, cancellationToken);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result);
        }

        [Authorize]
        [HttpPost("updateblogpost")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] BlogPostUpdateDto blogDto, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Message = "User ID not found." });

            var result = await _blogService.UpdateBlogPost(blogDto, userId, cancellationToken);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("deleteblogpost/{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Message = "User ID not found." });

            var result = await _blogService.DeleteBlogPost(id, userId, cancellationToken);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result);
        }
    }
}
