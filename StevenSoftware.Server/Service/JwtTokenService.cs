using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StevenSoftware.Server.Services
{
	public class JwtTokenService
	{
		private readonly IConfiguration _config;
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ILogger<JwtTokenService> logger)
		{
			_config = configuration;
			_userManager = userManager;
            _logger = logger;
        }

		public JwtTokenDto CreateTokenAsync(ApplicationUser user)
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