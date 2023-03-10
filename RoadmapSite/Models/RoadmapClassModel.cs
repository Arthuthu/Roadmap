namespace RoadmapSite.Models;

public class RoadmapClassModel
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Category { get; set; }
	public string? IsApproved { get; set; }
	public string? IsHidden { get; set; }


	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
				 
	public string? AuthorId { get; set; }
	public string? Author { get; set; }


	public string? RoadmapHtmlClass { get; set; }
	public int RoadmapTotalVotes { get; set; }


	public Guid UserId { get; set; }
}
