namespace RoadmapRepository.Models;

public class RoadmapClassModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public string? Category { get; set; }
	public string? IsApproved { get; set; }
	public string? IsHidden { get; set; }

	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }


	public Guid UserId { get; set; }
}
