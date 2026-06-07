using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = _config["Admin:Email"];
            var adminPassword = _config["Admin:Password"];

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                _logger.LogError("Admin credentials missing in configuration. Skipping seeding.");
                return;
            }

            try
            {
                var existingUser = await userManager.FindByEmailAsync(adminEmail);
                if (existingUser != null)
                {
                    _logger.LogInformation("Admin user already exists. Skipping seeding.");
                    return;
                }

                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create admin user: {Errors}", errors);
                    return;
                }

                _logger.LogInformation("Admin user created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while seeding admin user.");
            }
        }
    }
}