﻿namespace StevenSoftware.Server.Models.Dto
{
	public class LoginDto
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
        public string? RecaptchaToken { get; set; }
    }
}
