using System;
using MovieReview.Api.DTOs;
using MovieReview.Api.Model;

namespace MovieReview.Api.Extensions
{
	public static class CustomLinq
	{
		public static MovieReviewResponseDto AverageRating(this IQueryable<UserMovieReview> totalReviews, decimal review)
		{
			var reviews = totalReviews.Where(x => review == 0 || x.Review == review).Select(x=>x.Review).ToList();

			//var summedReviews = reviews.Sum();
			//var totalNumOfReviews = reviews.Count;

			//var avgResult = (summedReviews / totalNumOfReviews);

			var avgResult = reviews.Average();

            return new MovieReviewResponseDto(avgResult, reviews.Count) ;


		}
	}
}

