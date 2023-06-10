using System;
using Movie.Service.Nuget.Interface;

namespace MovieReview.Api.Interface
{
	public interface IUpdateInterface : IEventProcessor
	{
	}

    public interface IDeleteInterface : IEventProcessor
    {
    }

    public interface IAddDirectorInterface : IEventProcessor
    {
    }
}

