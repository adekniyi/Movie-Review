using System;
namespace MovieReview.Api.DTOs
{
	public class ReviewMovieRequestDto
	{
		public int UserId { get; set; }
		public int MovieId { get; set; }
		public decimal Review { get; set; }
	}
}

