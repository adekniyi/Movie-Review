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
            HandleMovie(message);
        }

        //I can have another method that checks for event type

        private void HandleMovie(string message)
        {
            using(var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMovieReviewRepository>();

                var publishedMessage = JsonSerializer.Deserialize<PublishDTO>(message);

                if(publishedMessage.ActionType == ActionType.Create)
                {
                    var model = new MovieReview.Api.Model.Movie
                    {
                        CreatedAt = DateTime.Now,
                        MovieForeignId = publishedMessage.Id,
                        Name = publishedMessage.Name
                    };


                    repo.AddMovie(model);
                    Console.WriteLine($"movie added successfully {model}");
                }else if(publishedMessage.ActionType == ActionType.Update)
                {
                    var model = new MovieReview.Api.Model.Movie
                    {
                        MovieForeignId = publishedMessage.Id,
                        Name = publishedMessage.Name
                    };

                    repo.UpdateMovie(model);
                    Console.WriteLine($"movie updated successfully {model}");
                }else if(publishedMessage.ActionType == ActionType.Delete)
                {
                    repo.DeleteMovie(publishedMessage.Id);

                    Console.WriteLine($"movie with id: {publishedMessage.Id} deleted successfully");
                }

                
            }
        }
    }
}

