using System;
using System.Text;
using System.Threading.Channels;
using MovieReview.Api.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MovieReview.Api.Repository
{
	public class MessageBusSubscriber: BackgroundService 
	{
        //private readonly IConfiguration _config;
        //      private readonly IEventProcessor _eventProcessor;

        //      private IConnection _connection;
        //      private IModel _channel;

        //private TestConsumer _consumer;
        private readonly IServiceScopeFactory _services;
        public MessageBusSubscriber(IServiceScopeFactory services) //IConfiguration config, IEventProcessor eventProcessor TestConsumer consumer
        {
            //_consumer = consumer;
            //_config = config;
            //_eventProcessor = eventProcessor;
            //InitializeRabbitMq();
            _services = services;
        }

        //private void InitializeRabbitMq()
        //{
        //    var factory = new ConnectionFactory() { HostName = _config["RabbiMQ:Host"], Port = int.Parse(_config["RabbiMQ:Port"]) };

        //    try
        //    {
        //        _connection = factory.CreateConnection();
        //        _channel = _connection.CreateModel();

        //        _channel.ExchangeDeclare(exchange: "trigger_movie", type: ExchangeType.Fanout);

        //        _channel.QueueDeclare(queue: "trigger_review_queue",
        //             durable: true,
        //             exclusive: false,
        //             autoDelete: false,
        //             arguments: null);

        //        _channel.QueueBind(queue: "trigger_review_queue", exchange: "trigger_movie", routingKey: "trigger_movie_create");

        //        Console.WriteLine("Listenting on message bus: queue name should follow");
        //        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        //        Console.WriteLine("Connected to massage bus");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            //var consumer = new EventingBasicConsumer(_channel);

            //consumer.Received += (moduleHandle, e) =>
            //{
            //    var body = e.Body;

            //    var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            //    Console.WriteLine($"Message receieved {notificationMessage}");

            //    _eventProcessor.ProcessEvent(notificationMessage);
            //};

            //_channel.BasicConsume(queue: "trigger_review_queue", autoAck: true, consumer: consumer);

            //_consumer.Consume();

            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<TestConsumer<EventProcessor>>();

                //repo._exchange = "trigger_movie";
                //repo._queue = "trigger_review_queue";
                //repo._routingKey = "trigger_movie_create";

                repo.InitializeRMQ("trigger_movie", "trigger_review_queue", "trigger_movie_create");
                repo.Consume();
            }
                return Task.CompletedTask;
        }


        //public void Dispose()
        //{
        //    if (_channel.IsOpen)
        //    {
        //        _channel.Close();
        //        _connection.Close();
        //    }
        //}
        //private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        //{
        //    Console.WriteLine("Shut down");
        //}


    }
}

