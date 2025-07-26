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
            {
                _logger.LogWarning("Image is null or empty.");
                return (false, null, "No image uploaded.");
            }

            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
            var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            _logger.LogInformation("Image extension: {Extension}", ext);

            if (!allowedExts.Contains(ext))
            {
                _logger.LogWarning("Image extension not allowed: {Extension}", ext);
                return (false, null, "Only JPG, PNG, or WEBP files are allowed.");
            }

            if (!Directory.Exists(_imageFolder))
            {
                _logger.LogInformation("Image folder does not exist. Creating: {ImageFolder}", _imageFolder);
                Directory.CreateDirectory(_imageFolder);
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(_imageFolder, fileName);

            _logger.LogInformation("Saving image to: {FilePath}", filePath);

            try
            {
                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream, cancellationToken);

                var imageUrl = $"/uploads/{fileName}";
                _logger.LogInformation("Image uploaded successfully: {ImageUrl}", imageUrl);
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
            _logger.LogInformation("DeleteMediaByFileName called with: {FileName}", fileName);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                _logger.LogWarning("FileName is null or whitespace.");
                return false;
            }

            var filePath = Path.Combine(_imageFolder, fileName);

            if (!File.Exists(filePath))
            {
                _logger.LogWarning("File not found: {FilePath}", filePath);
                return false;
            }

            try
            {
                File.Delete(filePath);
                _logger.LogInformation("File deleted: {FilePath}", filePath);
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
