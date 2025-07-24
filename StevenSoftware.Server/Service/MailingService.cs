using DotNetEnv;
using MailKit.Net.Smtp;
using MimeKit;
using StevenSoftware.Server.Models.Dto;

namespace StevenSoftware.Server.Service
{
    public class MailingService
    {

        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;

        public MailingService(IConfiguration config, IHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public async Task SendEmailAsync(MailDto dto)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(_config["Email:To"]));
            message.ReplyTo.Add(new MailboxAddress($"{dto.FirstName} {dto.LastName}", dto.Email));
            message.Subject = $"New message from {dto.FirstName} {dto.LastName}";

            message.Body = new TextPart("plain")
            {
                Text = $"From: {dto.FirstName} {dto.LastName}\nEmail: {dto.Email}\n\nMessage:\n{dto.Message}"
            };

            using var client = new SmtpClient();

            var smtpServer = _config["Email:SmtpServer"];
            var port = int.Parse(_config["Email:Port"] ?? "587");

            if (_env.IsDevelopment())
            {
                await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.None);
            }
            else
            {
                await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
