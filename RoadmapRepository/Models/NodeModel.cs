namespace Domain.Models;

public class NodeModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }


	public Guid RoadmapId { get; set; }
}
