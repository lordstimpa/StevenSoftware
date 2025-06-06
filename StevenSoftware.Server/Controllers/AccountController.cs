using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Models.Dto;
using StevenSoftware.Server.Services;
using System.Security.Claims;

namespace StevenSoftware.Server.Controllers
{
	[ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtTokenService _jwtTokenService;

		public AccountController(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService)
		{
			_userManager = userManager;
			_jwtTokenService = jwtTokenService;
		}

		[AllowAnonymous]
        [HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
				return Unauthorized(new { Message = "Invalid email." });

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized(new { Message = "Invalid password." });

			var token = await _jwtTokenService.CreateTokenAsync(user);

			return Ok(token);
		}

		[Authorize(Roles = "Admin")]
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userId))
				return BadRequest(new { Message = "User ID not found." });

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				return NotFound(new { Message = "User not found." });

			return Ok(new { user.Id, user.Email, user.UserName, user.FirstName, user.LastName });
		}

        [Authorize(Roles = "Admin")]
        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Message = "User ID not found." });

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            if (!string.IsNullOrWhiteSpace(userDto.FirstName))
                user.FirstName = userDto.FirstName;

            if (!string.IsNullOrWhiteSpace(userDto.LastName))
                user.LastName = userDto.LastName;

            if (!string.IsNullOrWhiteSpace(userDto.Email) && user.Email != userDto.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, userDto.Email);
                var setUserNameResult = await _userManager.SetUserNameAsync(user, userDto.Email);

                if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        Message = "Failed to update email.",
                        Errors = setEmailResult.Errors.Concat(setUserNameResult.Errors)
                    });
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update user." });

            return Ok(new { Message = "User updated successfully.", user });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateuserpassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] PasswordDto passwordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Message = "User ID not found." });

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            if (string.IsNullOrWhiteSpace(passwordDto.CurrentPassword) || string.IsNullOrWhiteSpace(passwordDto.NewPassword))
                return BadRequest(new { Message = "Both current and new passwords are required." });

            var result = await _userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Failed to change password." });
            }

            return Ok(new { Message = "Password updated successfully." });
        }
    }
}