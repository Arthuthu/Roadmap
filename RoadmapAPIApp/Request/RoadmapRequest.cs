namespace RoadmapAPIApp.Request;

public class RoadmapRequest
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }


	public Guid UserId { get; set; }
}
