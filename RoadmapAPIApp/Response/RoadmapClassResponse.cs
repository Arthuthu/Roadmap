namespace RoadmapAPIApp.Response;

public class RoadmapClassResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public string? Category { get; set; }
	public bool IsApproved { get; set; }
	public string? AuthorName { get; set; }


	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }


	public Guid UserId { get; set; }
}
