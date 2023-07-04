using System;
using System.Text;
using System.Threading.Channels;
using Movie.Service.Nuget.Interface;
using Movie.Service.Nuget.Repository;
using MovieReview.Api.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MovieReview.Api.Repository
{
	public class MessageBusSubscriber: BackgroundService 
	{
        private readonly IServiceScopeFactory _services;
        //private readonly MessageBusConsumer<IEventProcessor> _consumer;

        public MessageBusSubscriber(IServiceScopeFactory services) //, MessageBusConsumer<IEventProcessor> consumer
        {
            _services = services;
            //_consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();


            //Call this method for the "create" endpoint

           InitializeAndConsumeMovieCreated();

           // Call this method for the "update" endpoint
           InitializeAndConsumeMovieUpdated();

           // Call this method for the "delete" endpoint
           InitializeAndConsumeMovieDeleted();

            InitializeAndConsumeDirectorCreate();
            //using (var scope = _services.CreateScope())
            //{
            //    var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<IEventProcessor>>();




            //    repo.InitializeRMQ("trigger_movie", "trigger_review_queue", "trigger_movie_create");
            //    //repo.Consume();
            //    //repo.Dispose();

            //    repo.InitializeRMQ("trigger_movie", "trigger_update_queue", "trigger_movie_update");
            //    //repo.Consume();
            //    //repo.Dispose();

            //    repo.InitializeRMQ("trigger_movie", "trigger_delete_queue", "trigger_movie_delete");
            //    //repo.Consume();
            //    repo.Dispose();
            //}

            return Task.CompletedTask;
        }


        private void InitializeAndConsumeMovieCreated()
        {
            //_consumer.InitializeRMQ("trigger_movie", "trigger_review_queue", "trigger_movie_create");
            //_consumer.Consume();
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<ICreatInterface>>();
                repo.InitializeRMQ("trigger_movie", "trigger_review_queue", "trigger_movie_create");
                repo.Consume();
            }
        }

        private void InitializeAndConsumeMovieUpdated()
        {
            //_consumer.InitializeRMQ("trigger_movie", "trigger_update_queue", "trigger_movie_update");
            //_consumer.Consume();
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<IUpdateInterface>>();
                repo.InitializeRMQ("trigger_movie", "trigger_update_queue", "trigger_movie_update");
                repo.Consume();
            }
        }

        private void InitializeAndConsumeMovieDeleted()
        {
            //_consumer.InitializeRMQ("trigger_movie", "trigger_delete_queue", "trigger_movie_delete");
            //_consumer.Consume();

            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<IDeleteInterface>>();
                repo.InitializeRMQ("trigger_movie", "trigger_delete_queue", "trigger_movie_delete");
                repo.Consume();
            }
        }

        private void InitializeAndConsumeDirectorCreate()
        {
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<MessageBusConsumer<IAddDirectorInterface>>();
                repo.InitializeRMQ("trigger_director", "trigger_director_queue_create", "trigger_director_create");
                repo.Consume();
            }
        }


    }
}

