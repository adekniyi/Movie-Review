using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Service.Api.DTOs;
using Book.Service.Api.Interface;
using Book.Service.Api.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Book.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepo;
      

        public MovieController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
            //_directorRepo = directorRepo;
        }

     

        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieRequestDto model)
        {
            var result = await _movieRepo.CreateMovie(model);

            return result ? Ok("created successfully") : BadRequest("Unabale to create");
        }

        [HttpPost("create-movie-director")]
        public async Task<IActionResult> CreateMovieWithDirector(MovieWithDirectorRequestDto model)
        {
            var result = await _movieRepo.CreateMovieWithDirector(model);

            return result ? Ok("created successfully") : BadRequest("Unabale to create");
        }

      
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var result = await _movieRepo.GetMovies();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var result = await _movieRepo.GetMovie(id);

            return Ok(result);
        }

        [HttpPost("update-movie")]
        public async Task<IActionResult> UpdateMovie(MovieRequestDto model)
        {
            var result = await _movieRepo.UpdateMovie(model);

            return result ? Ok("created successfully") : BadRequest("Unabale to create");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var result = await _movieRepo.DeleteMovie(id);

            return Ok(result);
        }
    }
}

