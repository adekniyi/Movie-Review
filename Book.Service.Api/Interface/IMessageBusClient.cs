using System;
using Book.Service.Api.DTOs;

namespace Book.Service.Api.Interface
{
	public interface IMessageBusClient
	{
		void Publish(dynamic model, string routingKey);
		void Initialize(string exchange);
    }
}

