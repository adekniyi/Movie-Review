using System;
namespace Book.Service.Api.DTOs
{
	public class MovieRequestDto
	{
		public int MovieId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Category { get; set; }
		public int DirectorId { get; set; }
	}

	public class MovieWithDirectorRequestDto : MovieRequestDto
	{
		public DirectorRequestDto Director { get; set; }
	}
}

