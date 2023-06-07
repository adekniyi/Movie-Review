using System;
using MovieReview.Api.DTOs;

namespace MovieReview.Api.Interface
{
	public interface IMovieReviewRepository
	{
		Task<bool> AddMovieReview(ReviewMovieRequestDto model);
        MovieReviewResponseDto GetMovieReview(int movieId, decimal review);
        bool AddMovie(MovieReview.Api.Model.Movie model);
        List<MovieReview.Api.Model.Movie> GetMovies();
    }
}

