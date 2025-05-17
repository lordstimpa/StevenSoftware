using Microsoft.AspNetCore.Mvc;

namespace StevenSoftware.Server.Controllers
{
	[ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;

		public HealthController(ILogger<HealthController> logger)
		{
			_logger = logger;
		}

        [HttpGet("apihealth")]
        public IActionResult ApiHealth()
		{
			return Ok(new { status = "Healthy" });
		}
	}
}
