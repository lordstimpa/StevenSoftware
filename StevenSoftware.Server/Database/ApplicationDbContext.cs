using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StevenSoftware.Server.Models;

namespace StevenSoftware.Server.Database
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
