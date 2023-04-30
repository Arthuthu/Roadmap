namespace RoadmapRepository.Models;

public class DenunciaModel
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    
    public Guid AuthorId { get; set; }
    public string Username { get; set; }
    public Guid? RoadmapId { get; set; }
    public Guid? CommentId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
