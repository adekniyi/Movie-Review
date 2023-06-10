using System;
using MovieReview.Api.DTOs;
using MovieReview.Api.Model;

namespace MovieReview.Api.Interface
{
	public interface IMovieReviewRepository
	{
		Task<bool> AddMovieReview(ReviewMovieRequestDto model);
        MovieReviewResponseDto GetMovieReview(int movieId, decimal review);
        bool AddMovie(MovieReview.Api.Model.Movie model);
        List<MovieReview.Api.Model.Movie> GetMovies();
        bool UpdateMovie(MovieReview.Api.Model.Movie model);
        bool DeleteMovie(int id);
        bool AddDirector(Director model);
        List<Director> GetDirectors();
    }
}

