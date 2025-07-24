using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StevenSoftware.Server.Models.Dto;
using StevenSoftware.Server.Service;

namespace StevenSoftware.Server.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailingController : ControllerBase
    {
        private readonly MailingService _mailingService;
        private readonly AccountService _accountService;

        public MailingController(MailingService mailingService, AccountService accountService)
        {
            _mailingService = mailingService;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMail([FromBody] MailDto mailDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mailDto.RecaptchaToken))
                {
                    return BadRequest(new { Message = "Missing reCAPTCHA token." });
                }

                var isCaptchaValid = await _accountService.VerifyCaptcha(mailDto.RecaptchaToken);
                if (!isCaptchaValid)
                {
                    return BadRequest(new { Message = "Suspicious behavior detected. Please try again later." });
                }

                await _mailingService.SendEmailAsync(mailDto);
                return Ok(new { Message = "Mail sent successfully. I usually respond within 24 hours." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to send mail", Error = ex.Message });
            }
        }
    }
}
