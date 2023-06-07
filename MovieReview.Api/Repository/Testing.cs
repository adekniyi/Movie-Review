﻿using System;
using MovieReview.Api.Interface;
using RabbitMQ.Client;

namespace MovieReview.Api.Repository
{
	public class Testing
	{
        private readonly IConfiguration _config;

        public IConnection _connection;
        public IModel _channel;

        public Testing(IConfiguration config)
		{
            _config = config;
        }


        public void InitializeRabbitMq(string exchange, string queue, string routingKey)
        {
            var factory = new ConnectionFactory() { HostName = _config["RabbiMQ:Host"], Port = int.Parse(_config["RabbiMQ:Port"]) };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout); //"trigger_movie"

                _channel.QueueDeclare(queue: queue, //"trigger_review_queue"
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);//"trigger_review_queue", "trigger_movie", "trigger_movie_create"

                Console.WriteLine("Listenting on message bus: queue name should follow");
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected to massage bus");
            }
            catch (Exception ex)
            {

            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Shut down");
        }

    }
}

