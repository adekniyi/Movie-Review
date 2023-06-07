using System;
using Movie.Service.Nuget.Model;

namespace Book.Service.Api.Model
{
	public class Movie : IEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsDeleted { get; set; }
		public int Category { get; set; }
		public int DirectorId { get; set; }

		public Director Director { get; set; }
        public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

