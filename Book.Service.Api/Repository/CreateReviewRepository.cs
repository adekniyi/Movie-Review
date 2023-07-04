using System;
using System.Text.Json;
using Book.Service.Api.DTOs;
using Book.Service.Api.Interface;
using Book.Service.Api.Model;

namespace Book.Service.Api.Repository
{
    public class CreateReviewRepository : ICreateReviewRepository
    {
        private readonly IServiceScopeFactory _services;

        public CreateReviewRepository(IServiceScopeFactory services)
        {
            _services = services;
        }
        public void ProcessEvent(string message)
        {
            HandleMovieReview(message);
        }


        private void HandleMovieReview(string message)
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishReviewDTO>(message);


                if (publishedMessage.ActionType == ActionType.Create)
                {
                    var model = new MovieReview
                    {
                        ReviewForeignId = publishedMessage.Id,
                        MovieId = publishedMessage.MovieId,
                        Rating = publishedMessage.Rating,
                        CreatedAt = DateTime.Now,
                    };

                    repo.AddMovieReview(model);

                    Console.WriteLine($"review added successfully");
                }


            }
        }

    }
}

