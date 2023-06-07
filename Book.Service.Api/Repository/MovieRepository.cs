using System;
using System.IO;
using Book.Service.Api.Data;
using Book.Service.Api.DTOs;
using Book.Service.Api.Interface;
using Book.Service.Api.Model;
using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;
using Movie.Service.Nuget.Interface;

namespace Book.Service.Api.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApiDbContext _context;
        private readonly IGenericRepository<Book.Service.Api.Model.Movie> movieRepository;
        private readonly IMessageBusClient _messageBusClient;


        public MovieRepository(ApiDbContext context, IGenericRepository<Book.Service.Api.Model.Movie> movieRepository, IMessageBusClient messageBusClient)
        {
            _context = context;
            this.movieRepository = movieRepository;
            _messageBusClient = messageBusClient;
            _messageBusClient.Initialize("trigger_movie");
        }



        public async Task<bool> CreateMovie(MovieRequestDto model)
        {
            try
            {
                var movie = new Book.Service.Api.Model.Movie
                {
                    Category = model.Category,
                    Description = model.Description,
                    DirectorId = model.DirectorId,
                    Name = model.Name,
                    CreatedAt = DateTime.Now
                };

                var result = await movieRepository.CreateAsync(movie);

                _messageBusClient.Publish(new PublishDTO
                {
                    Event = "Publish_Movie",
                    Id = movie.Id,
                    Name = movie.Name,
                    ActionType = ActionType.Create
                }, "trigger_movie_create");

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateMovieWithDirector(MovieWithDirectorRequestDto model)
        {
            try
            {
                var movie = new Book.Service.Api.Model.Movie
                {
                    Category = model.Category,
                    Description = model.Description,
                    DirectorId = model.DirectorId,
                    Name = model.Name,
                    CreatedAt = DateTime.Now,
                    Director = new Director
                    {
                        Age = model.Director.Age,
                        Name = model.Director.Name,
                        CreatedAt = DateTime.Now
                    }
                };

                var result = await movieRepository.CreateAsync(movie);

                _messageBusClient.Publish(new PublishDTO
                {
                    Event = "Publish_Movie",
                    Id = movie.Id,
                    Name = movie.Name,
                    ActionType = ActionType.Create
                }, "trigger_movie_create");

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MovieWithDirectorResponseDto> GetMovie(int movieId)
        {
            var result = movieRepository.IQueryableOfT().ApplyIncludesOnQuery(x=>x.Director).ApplySinglePredicate(x => x.Id == movieId);

            return new MovieWithDirectorResponseDto
            {
                Category = result.Category,
                Description = result.Description,
                MovieId = result.Id,
                Name = result.Name,
                Director = new DirectorResponseDto
                {
                    age = result.Director.Age,
                    directorId = result.Director.Id,
                    name = result.Director.Name
                }
            };
        }

        public async Task<List<MovieWithDirectorResponseDto>> GetMovies()
        {
            var res = await movieRepository.IQueryableOfT().ApplyIncludesOnQuery(x => x.Director).Select(x => new MovieWithDirectorResponseDto
            {
                Category = x.Category,
                Description = x.Description,
                Director = new DirectorResponseDto
                {
                    age = x.Director.Age,
                    directorId = x.Director.Id,
                    name = x.Director.Name
                },
                MovieId = x.Id,
                Name = x.Name
            }).ToListAsync();

            return res;
        }
    }
}

