using StevenSoftware.Server.Models.Dto;
using System.Text.Json;

namespace StevenSoftware.Server.Service
{
    public class AccountService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IConfiguration configuration, HttpClient httpClient, ILogger<AccountService> logger)
        {
            _config = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> VerifyCaptcha(string token)
        {
            var secret = _config["GoogleReCaptcha:SecretKey"];

            try
            {
                var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}", null);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("reCAPTCHA verification failed with status code {StatusCode}", response.StatusCode);
                    return false;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaResult = JsonSerializer.Deserialize<RecaptchaResponseDto>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return captchaResult?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying reCAPTCHA token");
                return false;
            }
        }
    }
}
