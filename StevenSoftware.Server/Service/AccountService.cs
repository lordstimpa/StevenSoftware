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

            if (string.IsNullOrWhiteSpace(secret))
            {
                _logger.LogError("reCAPTCHA secret key is missing in configuration.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("reCAPTCHA token is missing.");
                return false;
            }

            try
            {
                var values = new Dictionary<string, string>
                {
                    { "secret", secret },
                    { "response", token }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await _httpClient.PostAsync(
                    "https://www.google.com/recaptcha/api/siteverify",
                    content
                );

                var jsonString = await response.Content.ReadAsStringAsync();

                var captchaResult = JsonSerializer.Deserialize<RecaptchaResponseDto>(
                    jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (captchaResult == null)
                {
                    _logger.LogWarning("reCAPTCHA returned null response: {Response}", jsonString);
                    return false;
                }

                if (!captchaResult.Success)
                {
                    _logger.LogWarning(
                        "reCAPTCHA failed. Response: {@CaptchaResult}",
                        captchaResult
                    );
                }

                return captchaResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying reCAPTCHA token");
                return false;
            }
        }
    }
}
