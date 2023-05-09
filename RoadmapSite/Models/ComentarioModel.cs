namespace RoadmapSite.Models;

public class ComentarioModel
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public string AuthorName { get; set; }
	public Guid AuthorId { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }

	public string ComentarioHtmlClass { get; set; }
	public int ComentarioTotalVotes { get; set; }
	
	public Guid? UserId { get; set; }
	public Guid RoadmapId { get; set; }
}
