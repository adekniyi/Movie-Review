using System;
using RabbitMQ.Client.Events;
using System.Threading.Channels;
using RabbitMQ.Client;
using System.Text;
using MovieReview.Api.Interface;

namespace MovieReview.Api.Repository
{
	public class TestConsumer<T> where T : IEventProcessor
    {
		private Testing _bus;
        private readonly T _eventProcessor;

        private string _queue;
        public TestConsumer(Testing bus, T eventProcessor)
        {
            _bus = bus;
            _eventProcessor = eventProcessor;
        }


        public void InitializeRMQ(string exchange, string queue, string routingKey)
        {
            _bus.InitializeRabbitMq(exchange, queue, routingKey);

            _queue = queue;
        }
        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_bus._channel);

            consumer.Received += (moduleHandle, e) =>
            {
                var body = e.Body;

                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                Console.WriteLine($"Message receieved {notificationMessage}");

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _bus._channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);
        }


        public void Dispose()
        {
            if (_bus._channel.IsOpen)
            {
                _bus._channel.Close();
                _bus._connection.Close();
            }
        }
    }
}

