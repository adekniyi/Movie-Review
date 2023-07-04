using System;
namespace MovieReview.Api.DTOs
{
	public class PublishDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
        public ActionType ActionType { get; set; }
    }

    public class PublishReviewDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public decimal Rating { get; set; }
        public int UserId { get; set; }
        public ActionType ActionType { get; set; }
    }

    public enum ActionType
    {
        Create,
        Update,
        Delete
    }
}

