namespace RoadmapSite.Models;

public class DenunciaModel
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }

    public Guid AuthorId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? RoadmapId { get; set; }
    public Guid? CommentId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
