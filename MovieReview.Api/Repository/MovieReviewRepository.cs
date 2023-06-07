using System;
using MovieReview.Api.DTOs;
using MovieReview.Api.Interface;
using MovieReview.Api.Data;
using MovieReview.Api.Model;
using Microsoft.EntityFrameworkCore;
using MovieReview.Api.Extensions;
using Movie.Service.Nuget.Interface;
using Movie.Service.Nuget.Extension;

namespace MovieReview.Api.Repository
{
    public class MovieReviewRepository : IMovieReviewRepository
    {
        private readonly ApiDbContext _context;
        private readonly IGenericRepository<UserMovieReview> _genericRepository;

        public MovieReviewRepository(ApiDbContext context, IGenericRepository<UserMovieReview> genericRepository)
        {
            _context = context; 
            _genericRepository = genericRepository;
        }
        public async Task<bool> AddMovieReview(ReviewMovieRequestDto model)
        {
            try
            {
                var hasReviewed =  _genericRepository.IQueryableOfT().ApplySinglePredicate(x => x.MovieId == model.MovieId && x.UserId == model.UserId);

                if(hasReviewed == null)
                {
                    var review = new UserMovieReview
                    {
                        Review = model.Review,
                        UserId = model.UserId,
                        MovieId = model.MovieId
                    };

                    return await _genericRepository.CreateAsync(review);
                }
                else
                {
                    hasReviewed.Review = model.Review;
                }

            

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public MovieReviewResponseDto GetMovieReview(int movieId, decimal review)
        {
            return _genericRepository.IQueryableOfT().Where(x => x.MovieId == movieId).AverageRating(review);
        }

        public bool AddMovie(MovieReview.Api.Model.Movie model)
        {
            try
            {
                 _context.Movies.Add(model);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MovieReview.Api.Model.Movie> GetMovies()
        {
            try
            {
                return _context.Movies.ToList(); 
            }
            catch (Exception ex)
            {
                return new List<Model.Movie>();
            }
        }
    }
}

