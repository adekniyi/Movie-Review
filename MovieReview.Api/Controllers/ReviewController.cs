using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Api.DTOs;
using MovieReview.Api.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieReview.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMovieReviewRepository _movieReviewRepo;

        public ReviewController(IMovieReviewRepository movieReviewRepo)
        {
            _movieReviewRepo = movieReviewRepo;
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddBookReview(ReviewMovieRequestDto model)
        {
            var result = await _movieReviewRepo.AddMovieReview(model);

            return result ? Ok("Success") : BadRequest("failed");
        }

        [HttpPost("get-movie-reviews")]
        public IActionResult GetMovieReview(MovieReviewFilterDTO model)
        {
            var result = _movieReviewRepo.GetMovieReview(model.MovieId, model.Review);

            return Ok(result);
        }

        [HttpGet("get-movie")]
        [Authorize]
        public IActionResult GetMovies()
        {
            var result = _movieReviewRepo.GetMovies();

            return Ok(result);
        }

        [HttpGet("get-directors")]
        public IActionResult GetDirectors()
        {
            var result = _movieReviewRepo.GetDirectors();

            return Ok(result);
        }

    }
}

