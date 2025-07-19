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

        public MailingController(MailingService mailingService)
        {
            _mailingService = mailingService;
        }

        [AllowAnonymous]
        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMail([FromBody] MailDto mailDto)
        {
            try
            {
                await _mailingService.SendEmailAsync(mailDto);
                return Ok(new { Message = "Mail sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to send mail", Error = ex.Message });
            }
        }

    }
}
