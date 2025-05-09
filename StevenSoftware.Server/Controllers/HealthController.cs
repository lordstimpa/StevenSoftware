using Microsoft.AspNetCore.Mvc;

namespace StevenSoftware.Server.Controllers
{
	[ApiController]
	[Route("api/health")]
	public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;

		public HealthController(ILogger<HealthController> logger)
		{
			_logger = logger;
		}

		[HttpGet("ApiHealth")]
		public IActionResult ApiHealth()
		{
			return Ok(new { status = "Healthy" });
		}
	}
}
