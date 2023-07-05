using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Model;

namespace MovieReview.Api.Data
{
	public class ApiDbContext : DbContext
	{
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

		public DbSet<UserMovieReview> UserMovieReviews { get; set; }
		public DbSet<MovieReview.Api.Model.Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Movie>().HasQueryFilter(x => !x.IsDeleted);
        //    modelBuilder.Entity<Director>().HasQueryFilter(x => !x.IsDeleted);

        //}
    }
}

