using System;
namespace Book.Service.Api.DTOs
{
	public class DirectorResponseDto
	{
		public int directorId { get; set; }
		public string name { get; set; }
		public int age { get; set; }

    };

	public class DirectorWithMoviesResponseDto : DirectorResponseDto
	{
		public List<MovieResponseDto> Movies { get; set; }
	};
}

