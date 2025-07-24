namespace StevenSoftware.Server.Models.Dto
{
    public class MailDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string? RecaptchaToken { get; set; }
    }
}
