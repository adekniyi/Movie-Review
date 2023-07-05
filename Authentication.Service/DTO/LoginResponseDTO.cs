using System;
namespace Authentication.Service.DTO
{
	public class LoginResponseDTO
	{
		public bool Success { get; set; }
		public string Token { get; set; }
	}

	public class SignUpRequestDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
	}
}

