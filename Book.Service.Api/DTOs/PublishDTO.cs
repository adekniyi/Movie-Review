using System;
namespace Book.Service.Api.DTOs
{
	public class PublishDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Event { get; set; }
		public ActionType ActionType { get; set; }
	}

	public enum ActionType
	{
		Create,
		Update,
		Delete
	}
}

