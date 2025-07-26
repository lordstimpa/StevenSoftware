using StevenSoftware.Server.Models;

namespace StevenSoftware.Server.Service
{
    public class MediaService
    {
        private readonly string _imageFolder;

        public MediaService(IWebHostEnvironment env)
        {
            _imageFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
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

                Console.WriteLine("Saving image to: " + _imageFolder);
                var imageUrl = $"/uploads/{fileName}";
                return (true, imageUrl, null);
            }
            catch (Exception ex)
            {
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
            catch
            {
                return false;
            }
        }
    }
}
