using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StevenSoftware.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StevenSoftware.Server.Services
{
	public class JwtTokenService
	{
		private readonly IConfiguration _config;
		private readonly UserManager<ApplicationUserModel> _userManager;

		public JwtTokenService(IConfiguration configuration, UserManager<ApplicationUserModel> userManager)
		{
			_config = configuration;
			_userManager = userManager;
		}

		public async Task<JwtTokenDto> CreateTokenAsync(ApplicationUserModel user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var key = new SymmetricSecurityKey(Convert.FromBase64String(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpireMinutes"])),
				signingCredentials: creds
			);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return new JwtTokenDto { AccessToken = tokenString };
		}
	}
}