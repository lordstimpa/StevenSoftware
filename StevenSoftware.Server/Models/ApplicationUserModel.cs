using Microsoft.AspNetCore.Identity;

namespace StevenSoftware.Server.Models
{
	public class ApplicationUserModel : IdentityUser 
	{
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
	}
}
