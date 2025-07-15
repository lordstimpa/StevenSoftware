using Microsoft.AspNetCore.Identity;
using StevenSoftware.Server.Models;

namespace StevenSoftware.Server.Services
{
	public class SeedingService
	{
		private readonly IConfiguration _config;

		public SeedingService(IConfiguration configuration)
		{
			_config = configuration;
		}

		public async Task SeedAdminUser(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var adminEmail = _config["Admin:Email"];
			var adminPassword = _config["Admin:Password"];

			if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
			{
				throw new Exception("Admin:Email or Admin:Password not configured in appsettings.json");
			}

			if (await userManager.FindByEmailAsync(adminEmail) == null)
			{
				var adminUser = new ApplicationUser
				{
					UserName = adminEmail,
					Email = adminEmail
				};
				var result = await userManager.CreateAsync(adminUser, adminPassword);
				if (!result.Succeeded)
				{
					throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
				}
			}
		}
	}
}