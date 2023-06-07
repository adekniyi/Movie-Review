using System;
using Movie.Service.Nuget.Model;

namespace MovieReview.Api.Model
{
	public class Movie : IEntity
    {
		public int MovieForeignId { get; set; }
		public string Name { get; set; }
        public int Id { get; set ; }
        public bool IsDeleted { get ; set; }
        public DateTime CreatedAt { get; set; }
    }
}

