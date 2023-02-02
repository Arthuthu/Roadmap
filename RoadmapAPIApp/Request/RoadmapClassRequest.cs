namespace RoadmapAPIApp.Request;

public class RoadmapClassRequest
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public string? Category { get; set; }


	public Guid UserId { get; set; }
}
