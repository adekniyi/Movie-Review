using System;
using Book.Service.Api.Interface;
using Movie.Service.Nuget.Interface;
using Movie.Service.Nuget.Repository;

namespace Book.Service.Api.Repository
{
	public class MessageBusSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory _services;

        public MessageBusSubscriber(IServiceScopeFactory services)
        {
            _services = services;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();


            InitializeAndConsumeMovieReviewCreated();

            return Task.CompletedTask;
        }

        private void InitializeAndConsumeMovieReviewCreated()
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<ICreateReviewRepository>>();
                repo.InitializeRMQ("trigger_review", "trigger_movie_review_queue", "trigger_review_movie_create");
                repo.Consume();
            }
        }


    }
}

