using System;
namespace MovieReview.Api.Interface
{
	public interface IEventProcessor
	{
		void ProcessEvent(string message);
	}
}

