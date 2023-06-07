using System;
namespace Book.Service.Api.DTOs
{
	public class DirectorRequestDto
	{
		public int DirectorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class DirectorWithMoviesRequestDto : DirectorRequestDto
    {
        public List<MovieRequestDto> Movies { get; set; }
    }
}

