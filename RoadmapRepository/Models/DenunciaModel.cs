namespace RoadmapRepository.Models;

public class DenunciaModel
{
    public Guid Id { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;

    public Guid AuthorId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid? RoadmapId { get; set; }
    public Guid? CommentId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
