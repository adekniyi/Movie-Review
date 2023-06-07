using System;
using Movie.Service.Nuget.Model;

namespace MovieReview.Api.Model
{
	public class UserMovieReview : IEntity
	{
		public int UserId { get; set; }
		public int MovieId { get; set; }
		public decimal Review { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get ; set; }
        public DateTime CreatedAt { get; set; }
    }
}

