﻿using System;
using System.Text.Json;
using Movie.Service.Nuget.Interface;
using MovieReview.Api.DTOs;
using MovieReview.Api.Interface;

namespace MovieReview.Api.Repository
{
	public class EventProcessor : IEventProcessor
	{
        private readonly IServiceScopeFactory _services;

        public EventProcessor(IServiceScopeFactory services)
        {
            _services = services;
        }

        public void ProcessEvent(string message)
        {
            AddMovie(message);
        }

        //I can have another method that checks for event type

        private void AddMovie(string message)
        {
            using(var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieReviewRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishDTO>(message);

                var model = new MovieReview.Api.Model.Movie
                {
                    CreatedAt = DateTime.Now,
                    MovieForeignId = publishedMessage.Id,
                    Name = publishedMessage.Name
                };


                repo.AddMovie(model);

                Console.WriteLine($"movie added successfully {model}");
            }
        }
    }
}

