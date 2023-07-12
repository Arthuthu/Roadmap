namespace RoadmapAPIApp.Request;

public class NodeRequest
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public Guid RoadmapId { get; set; }
}
