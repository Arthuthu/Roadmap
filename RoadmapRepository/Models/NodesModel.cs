namespace RoadmapRepository.Models;

public class NodesModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }


	public RoadmapModel RoadMap { get; set; }
}
