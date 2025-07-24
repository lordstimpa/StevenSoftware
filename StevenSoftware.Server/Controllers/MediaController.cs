using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StevenSoftware.Server.Service;

namespace StevenSoftware.Server.Controllers
{
    [ApiController]
    [Route("api/media")]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _mediaService;

        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [Authorize]
        [HttpPost("uploadimage")]
        public async Task<IActionResult> UploadImage(IFormFile image, CancellationToken cancellationToken)
        {
            var result = await _mediaService.UploadImageAsync(image, cancellationToken);
            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(new { imageUrl = result.ImageUrl, message = "File uploaded successfully." });
        }

        [Authorize]
        [HttpDelete("deletemedia/{fileName}")]
        public IActionResult DeleteMedia(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Filename is required.");

            var actualFileName = Path.GetFileName(fileName);
            var deleted = _mediaService.DeleteMediaByFileName(actualFileName);
            if (!deleted)
                return NotFound("File not found or could not be deleted.");

            return Ok(new { message = "File deleted successfully." });
        }
    }
}
