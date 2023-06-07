using System;
using Movie.Service.Nuget.Model;

namespace Book.Service.Api.Model
{
	public class Director : IEntity
    {
		
		public string Name { get; set; }
		public int Age { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Movie> Movies { get; set; }
    }
}

