namespace RoadmapRepository.Models;

public class RoadmapModel
{
	public Guid Id { get; set; }
	public string? Description { get; set; }


	public List<NodesModel> Nodes { get; set; }
	public UserModel User { get; set; }
}
