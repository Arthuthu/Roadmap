namespace RoadmapRepository.Models;

public class ComentarioModel
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public string AuthorName { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }

	public Guid UserId { get; set; }
	public Guid RoadmapId { get; set; }
}
