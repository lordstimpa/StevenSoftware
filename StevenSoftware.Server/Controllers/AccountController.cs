using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Services;
using System.Security.Claims;

namespace StevenSoftware.Server.Controllers
{
	[ApiController]
	[Route("api/account")]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUserModel> _userManager;
		private readonly JwtTokenService _jwtTokenService;

		public AccountController(UserManager<ApplicationUserModel> userManager, JwtTokenService jwtTokenService)
		{
			_userManager = userManager;
			_jwtTokenService = jwtTokenService;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
			{
				return Unauthorized(new { Message = "Invalid email or password" });
			}

			var token = await _jwtTokenService.CreateTokenAsync(user);
			return Ok(token);
		}

		[Authorize]
		[HttpGet("getuser")]
		public async Task<IActionResult> GetUser()
		{
			Console.WriteLine("Claims in User:");
			foreach (var claim in User.Claims)
			{
				Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
			}

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			Console.WriteLine($"Extracted userId from ClaimTypes.NameIdentifier: {userId ?? "null"}");

			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest(new { Message = "User ID not found" });
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound(new { Message = "User not found" });
			}

			return Ok(new { user.Id, user.Email, user.UserName });
		}
	}
}