namespace RoadmapAPIApp.Request;

public class ComentarioRequest
{
	public string? Description { get; set; }
	public Guid UserId { get; set; }
	public Guid RoadmapId { get; set; }

}

