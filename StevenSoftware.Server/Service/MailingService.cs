using MailKit.Net.Smtp;
using MimeKit;
using StevenSoftware.Server.Models.Dto;

namespace StevenSoftware.Server.Service
{
    public class MailingService
    {

        private readonly IConfiguration _config;

        public MailingService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(MailDto dto)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(_config["Email:To"]));
            message.Subject = $"New message from {dto.FirstName} {dto.LastName}";

            message.Body = new TextPart("plain")
            {
                Text = $"From: {dto.FirstName} {dto.LastName}\nEmail: {dto.Email}\n\nMessage:\n{dto.Message}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["Email:SmtpServer"], 1025, MailKit.Security.SecureSocketOptions.None);
            // await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
