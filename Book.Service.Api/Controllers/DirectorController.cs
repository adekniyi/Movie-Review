using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Service.Api.DTOs;
using Book.Service.Api.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Book.Service.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepo;

        public DirectorController(IDirectorRepository directorRepo)
        {
            _directorRepo = directorRepo;
        }


        [HttpPost]
        public async Task<IActionResult> CreateDirector(DirectorRequestDto model)
        {
            var result = await _directorRepo.CreateDirector(model);

            return result ? Ok("created successfully") : BadRequest("Unabale to create");
        }

        [HttpPost("create-director-movie")]
        public async Task<IActionResult> CreateDirectorWithMovies(DirectorWithMoviesRequestDto model)
        {
            var result = await _directorRepo.CreateDirectorWithMovies(model);


            return result ? Ok("created successfully") : BadRequest("Unabale to create");
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            var result = await _directorRepo.GetDirectors();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirector(int id)
        {
            var result = await _directorRepo.GetDirector(id);

            return Ok(result);
        }
    }
}

