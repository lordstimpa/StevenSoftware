using Microsoft.AspNetCore.Mvc;

namespace StevenSoftware.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HealthCheckController : ControllerBase
	{
		private readonly ILogger<HealthCheckController> _logger;

		public HealthCheckController(ILogger<HealthCheckController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(new { status = "Healthy" });
		}
	}
}
