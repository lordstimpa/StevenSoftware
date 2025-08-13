namespace StevenSoftware.Server.Models.Dto
{
    public class RecaptchaResponseDto
    {
        public bool Success { get; set; }
        public DateTime ChallengeTs { get; set; }
        public string Hostname { get; set; }
        public List<string>? ErrorCodes { get; set; }
    }
}
