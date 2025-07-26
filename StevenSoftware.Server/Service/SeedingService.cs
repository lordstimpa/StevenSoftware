using Microsoft.AspNetCore.Identity;
using StevenSoftware.Server.Models;

namespace StevenSoftware.Server.Services
{
	public class SeedingService
	{
		private readonly IConfiguration _config;
        private readonly ILogger<SeedingService> _logger;

        public SeedingService(IConfiguration configuration, ILogger<SeedingService> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task SeedAdminUser(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var adminEmail = _config["Admin:Email"];
			var adminPassword = _config["Admin:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                _logger.LogError("Admin credentials not found in appsettings.json");
                throw new Exception("Admin:Email or Admin:Password not found in appsettings.json");
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
                    var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create admin user: {Errors}", errorMessages);
                    throw new Exception("Failed to create admin user: " + errorMessages);
                }
            }
            else
            {
                _logger.LogInformation("Admin user with email {Email} already exists. Skipping creation.", adminEmail);
            }
        }
	}
}