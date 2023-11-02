namespace RoadmapAPI.Request;

public class DenunciaRequest
{
	public Guid Id { get; set; }
	public string? Description { get; set; } = string.Empty;
	public string? Type { get; set; } = string.Empty;

	public Guid AuthorId { get; set; }
	public Guid? UserId { get; set; }
	public Guid? RoadmapId { get; set; }
	public Guid? CommentId { get; set; }

	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
