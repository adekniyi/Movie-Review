using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Service.DTO;
using Authentication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }


        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpRequestDTO model)
        {
            var result = await _authRepo.SignUp(model.Email, model.FullName, model.Password);

            return result ? Ok("success") : BadRequest("error occured");
        }

        [HttpPost("log-in")]
        public async Task<IActionResult> SignIn(SignUpRequestDTO model)
        {
            var result = await _authRepo.Login(model.Email, model.Password);

            return result.Success ? Ok(result) : BadRequest("error occured");
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Testing if Auth works");
        }
    }
}

