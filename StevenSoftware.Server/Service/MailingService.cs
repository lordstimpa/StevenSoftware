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
        private readonly ILogger<MailingService> _logger;

        public MailingService(IConfiguration config, IHostEnvironment env, ILogger<MailingService> logger)
        {
            _config = config;
            _env = env;
            _logger = logger;
        }

        public async Task SendEmailAsync(MailDto dto)
        {
            _logger.LogInformation(
                "Sending email with values: FirstName='{FirstName}', LastName='{LastName}', Email='{Email}', Message='{Message}', RecaptchaToken='{Token}'",
                dto.FirstName ?? "NULL",
                dto.LastName ?? "NULL",
                dto.Email ?? "NULL",
                dto.Message ?? "NULL",
                dto.RecaptchaToken ?? "NULL"
            );

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
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email from {From} to {To}", dto.Email, _config["Email:To"]);
                throw;
            }
        }
    }
}
