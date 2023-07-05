using System;
using Authentication.Service.DTO;

namespace Authentication.Service.Interface
{
	public interface IAuthRepository
	{
		Task<bool> SignUp(string email, string fullName, string password);
		Task<LoginResponseDTO> Login(string email, string password);
	}
}

