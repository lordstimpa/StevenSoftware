using Microsoft.AspNetCore.Mvc;

namespace StevenSoftware.Server.Controllers
{
    [ApiController]
    [Route("api/media")]
    public class MeidaController : ControllerBase
    {
        private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        [HttpPost]
        [Route("uploadimage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var ext = Path.GetExtension(image.FileName).ToLower();
            var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (Array.IndexOf(allowedExts, ext) < 0)
                return BadRequest("Only JPG, PNG, or WEBP files are allowed.");

            if (!Directory.Exists(_imageFolder))
                Directory.CreateDirectory(_imageFolder);

            var fileName = Guid.NewGuid() + ext;
            var filePath = Path.Combine(_imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/{fileName}";

            return Ok(new { imageUrl });
        }
    }
}
