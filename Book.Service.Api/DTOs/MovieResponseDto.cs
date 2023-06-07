using System;
namespace Book.Service.Api.DTOs
{
	public class MovieResponseDto
	{
		public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
    }

    public class MovieWithDirectorResponseDto : MovieResponseDto
    {
        public DirectorResponseDto Director { get; set; }
    }
}

