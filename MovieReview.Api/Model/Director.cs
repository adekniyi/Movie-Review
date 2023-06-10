using System;
using Movie.Service.Nuget.Model;

namespace MovieReview.Api.Model
{
	public class Director : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int DirectorForeignId { get; set; }
		public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

