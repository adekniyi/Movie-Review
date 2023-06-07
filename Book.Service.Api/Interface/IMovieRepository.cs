using System;
using Book.Service.Api.DTOs;

namespace Book.Service.Api.Interface
{
	public interface IMovieRepository
	{
		Task<bool> CreateMovie(MovieRequestDto model);
		Task<bool> CreateMovieWithDirector(MovieWithDirectorRequestDto model);

     

		Task<List<MovieWithDirectorResponseDto>> GetMovies();
		Task<MovieWithDirectorResponseDto> GetMovie(int movieId);
    }
}

