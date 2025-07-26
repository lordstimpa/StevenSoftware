using Microsoft.Extensions.Logging;

namespace StevenSoftware.Server.Service
{
    public class MediaService
    {
        private readonly string _imageFolder;
        private readonly ILogger<MediaService> _logger;

        public MediaService(IWebHostEnvironment env, ILogger<MediaService> logger)
        {
            _imageFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
            _logger = logger;
        }

        public async Task<(bool Success, string? ImageUrl, string? Error)> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image == null || image.Length == 0)
                return (false, null, "No image uploaded.");

            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
            var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExts.Contains(ext))
                return (false, null, "Only JPG, PNG, or WEBP files are allowed.");

            if (!Directory.Exists(_imageFolder))
                Directory.CreateDirectory(_imageFolder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(_imageFolder, fileName);

            try
            {
                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream, cancellationToken);

                var imageUrl = $"/uploads/{fileName}";
                return (true, imageUrl, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving image to {FilePath}", filePath);
                return (false, null, $"Error saving file: {ex.Message}");
            }
        }

        public bool DeleteMediaByFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var filePath = Path.Combine(_imageFolder, fileName);

            if (!File.Exists(filePath))
                return false;

            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image from {FilePath}", filePath);
                return false;
            }
        }
    }
}
