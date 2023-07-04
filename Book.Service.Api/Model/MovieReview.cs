using System;
using Movie.Service.Nuget.Model;

namespace Book.Service.Api.Model
{
	public class MovieReview : IEntity
	{
		public int Id { get; set; }
		public int ReviewForeignId { get; set; }
		public int MovieId { get; set; }
		public decimal Rating { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

