using Microsoft.AspNetCore.Identity;
using StevenSoftware.Server.Models;

namespace StevenSoftware.Server.Services
{
	public class SeedingService
	{
		private readonly IConfiguration _config;
		private readonly UserManager<ApplicationUserModel> _userManager;

		public SeedingService(IConfiguration configuration, UserManager<ApplicationUserModel> userManager)
		{
			_config = configuration;
			_userManager = userManager;
		}

		public async Task SeedAdminUser(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUserModel>>();

			var adminEmail = _config["Seed:Email"];
			var adminPassword = _config["Seed:Password"];

			if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
			{
				throw new Exception("Seed:Email or Seed:Password not configured in appsettings.json");
			}

			if (await userManager.FindByEmailAsync(adminEmail) == null)
			{
				var adminUser = new ApplicationUserModel
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