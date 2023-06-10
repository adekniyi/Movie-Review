using System;
using Book.Service.Api.DTOs;
using Book.Service.Api.Interface;
using Book.Service.Api.Model;
using Microsoft.EntityFrameworkCore;
using Movie.Service.Nuget.Extension;
using Movie.Service.Nuget.Interface;
using Movie.Service.Nuget.Repository;

namespace Book.Service.Api.Repository
{
	public class DirectorRepository : IDirectorRepository
    {
        private readonly IGenericRepository<Director> _repo;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMessageBusClient _directorMessageBusClient;

        public DirectorRepository(IGenericRepository<Director> repo, IMessageBusClient messageBusClient, IMessageBusClient directorMessageBusClient)
        {
            _repo = repo;
            _messageBusClient = messageBusClient;
            _directorMessageBusClient = directorMessageBusClient;
            _messageBusClient.Initialize("trigger_movie");
            _directorMessageBusClient.Initialize("trigger_director");
        }

        public async Task<bool> CreateDirector(DirectorRequestDto model)
        {
            try
            {
                var director = new Director
                {
                    Age = model.Age,
                    CreatedAt = DateTime.Now,
                    Name = model.Name
                };


                var result =  await _repo.CreateAsync(director);


                _directorMessageBusClient.Publish(new PublishDTO
                {
                    Event = "Publish_Director",
                    Id = director.Id,
                    Name = director.Name,
                    ActionType = ActionType.Create
                }, "trigger_director_create");


                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateDirectorWithMovies(DirectorWithMoviesRequestDto model)
        {
            try
            {
                var director = new Director
                {
                    Age = model.Age,
                    Name = model.Name,
                    CreatedAt = DateTime.Now,
                    Movies = model.Movies.Select(x => new Book.Service.Api.Model.Movie
                    {
                        Category = x.Category,
                        Description = x.Description,
                        Name = x.Name,
                        CreatedAt = DateTime.Now
                    }).ToList()
                };

                var result = await _repo.CreateAsync(director);


                foreach (var movie in director.Movies)
                {
                    _messageBusClient.Publish(new PublishDTO
                    {
                        Event = "Publish_Movie",
                        Id = movie.Id,
                        Name = movie.Name,
                        ActionType = ActionType.Create
                    }, "trigger_movie_create");
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<DirectorWithMoviesResponseDto> GetDirector(int directorId)
        {
            var x = _repo.IQueryableOfT().ApplyIncludesOnQuery(x => x.Movies).ApplySinglePredicate(x => x.Id == directorId);

            var res = new DirectorWithMoviesResponseDto
            {
                age = x.Age,
                directorId = x.Id,
                name = x.Name,
                Movies = x.Movies.Select(m => new MovieResponseDto
                {
                    MovieId = m.Id,
                    Category = m.Category,
                    Description = m.Description,
                    Name = m.Name
                }).ToList()
            };


            return res;
        }

        public async Task<List<DirectorWithMoviesResponseDto>> GetDirectors()
        {
            var res = await _repo.IQueryableOfT().ApplyIncludesOnQuery(x => x.Movies).Select(x => new DirectorWithMoviesResponseDto
            {
                age = x.Age,
                directorId = x.Id,
                name = x.Name,
                Movies = x.Movies.Select(m => new MovieResponseDto
                {
                    MovieId = m.Id,
                    Category = m.Category,
                    Description = m.Description,
                    Name = m.Name
                }).ToList()
            }).ToListAsync();

            return res;
        }


    }
}

