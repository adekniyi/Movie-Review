using System;
using Book.Service.Api.DTOs;

namespace Book.Service.Api.Interface
{
	public interface IDirectorRepository
	{
        Task<bool> CreateDirector(DirectorRequestDto model);
        Task<bool> CreateDirectorWithMovies(DirectorWithMoviesRequestDto model);

        Task<List<DirectorWithMoviesResponseDto>> GetDirectors();
        Task<DirectorWithMoviesResponseDto> GetDirector(int directorId);
    }
}

