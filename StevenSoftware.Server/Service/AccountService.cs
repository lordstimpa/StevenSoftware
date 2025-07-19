using StevenSoftware.Server.Models.Dto;
using System.Text.Json;

namespace StevenSoftware.Server.Service
{
    public class AccountService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public AccountService(IConfiguration configuration, HttpClient httpClient)
        {
            _config = configuration;
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyCaptcha(string token)
        {
            var secret = _config["GoogleReCaptcha:SecretKey"];
            try
            {
                var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}", null);

                if (!response.IsSuccessStatusCode) return false;

                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaResult = JsonSerializer.Deserialize<RecaptchaResponseDto>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var minScore = double.TryParse(_config["GoogleReCaptcha:MinScore"], out var score) ? score : 0.7;

                return (captchaResult?.Success ?? false) && (captchaResult.Score >= minScore);
            }
            catch
            {
                return false;
            }
        }
    }
}
