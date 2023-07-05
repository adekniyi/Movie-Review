using System;
using Authentication.Service.DTO;
using Authentication.Service.Interface;
using Authentication.Service.Model;
using Microsoft.AspNetCore.Identity;
using Movie.Service.Nuget.Interface;

namespace Authentication.Service.Repository
{
	public class AuthRepository : IAuthRepository
	{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTRepo _tokenGenerator;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, IJWTRepo tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDTO> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new LoginResponseDTO
                {
                    Success = false
                };
            }

            var checkForPassword = await _signInManager.CheckPasswordSignInAsync(user, password,false);

            if (!checkForPassword.Succeeded)
            {
                return new LoginResponseDTO
                {
                    Success = false
                };
            }

            var token =  _tokenGenerator.GenerateUserToken(new Movie.Service.Nuget.Model.User
            {
                Email = user.Email,
                FullName = user.FullName
            });

            return new LoginResponseDTO
            {
                Success = true,
                Token = token
            };
        }

        public async Task<bool> SignUp(string email, string fullName, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                return false;
            }

            var userDetails = new User
            {
                Email = email,
                UserName = email,
                NormalizedEmail = email,
                FullName = fullName,
                CreatedBy = DateTime.Now,
                UpdatedBy = DateTime.Now,
                EmailConfirmed = true
            };

            var userSuccessfullyCreated = await _userManager.CreateAsync(userDetails, password);

            if (userSuccessfullyCreated.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

