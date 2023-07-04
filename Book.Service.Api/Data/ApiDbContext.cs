using System;
using Book.Service.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Book.Service.Api.Data
{
	public class ApiDbContext : DbContext
	{
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

		public DbSet<Book.Service.Api.Model.Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>().HasQueryFilter(x => !x.IsDeleted);
            //modelBuilder.Entity<Director>().HasQueryFilter(x => !x.IsDeleted);

        }

        //public virtual DbSet<TEntity> Set<TEntity>() where TEntity : IEntity =>
        //    (DbSet<TEntity>)((IDbSetCache)this);
    }
}

