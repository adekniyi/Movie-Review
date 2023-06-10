using System;
using System.Text.Json;
using Movie.Service.Nuget.Interface;
using MovieReview.Api.DTOs;
using MovieReview.Api.Interface;
using MovieReview.Api.Model;

namespace MovieReview.Api.Repository
{
    public class UpdateRepository : IUpdateInterface
    {
        private readonly IServiceScopeFactory _services;

        public UpdateRepository(IServiceScopeFactory services)
        {
            _services = services;
        }
        public void ProcessEvent(string message)
        {
            HandleMovie(message);
        }

        private void HandleMovie(string message)
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieReviewRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishDTO>(message);


                if (publishedMessage.ActionType == ActionType.Update)
                {
                    var model = new MovieReview.Api.Model.Movie
                    {
                        MovieForeignId = publishedMessage.Id,
                        Name = publishedMessage.Name
                    };

                    repo.UpdateMovie(model);
                    Console.WriteLine($"movie updated successfully {model}");
                }

            }
        }

    }


    public class DeleteRepository : IDeleteInterface
    {
        private readonly IServiceScopeFactory _services;

        public DeleteRepository(IServiceScopeFactory services)
        {
            _services = services;
        }
        public void ProcessEvent(string message)
        {
            HandleMovie(message);
        }

        private void HandleMovie(string message)
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieReviewRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishDTO>(message);


                if (publishedMessage.ActionType == ActionType.Delete)
                {
                    repo.DeleteMovie(publishedMessage.Id);

                    Console.WriteLine($"movie with id: {publishedMessage.Id} deleted successfully");
                }


            }
        }

    }


    public class AddDirectorRepository : IAddDirectorInterface
    {
        private readonly IServiceScopeFactory _services;

        public AddDirectorRepository(IServiceScopeFactory services)
        {
            _services = services;
        }
        public void ProcessEvent(string message)
        {
            Console.WriteLine($"director is being handled");
            HandleDirector(message);
        }

        private void HandleDirector(string message)
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieReviewRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishDTO>(message);


                if (publishedMessage.ActionType == ActionType.Create)
                {
                    var model = new Director
                    {
                        DirectorForeignId = publishedMessage.Id,
                        Name = publishedMessage.Name,
                        CreatedAt = DateTime.Now,
                    };

                    repo.AddDirector(model);

                    Console.WriteLine($"director added successfully");
                }


            }
        }

    }
}

