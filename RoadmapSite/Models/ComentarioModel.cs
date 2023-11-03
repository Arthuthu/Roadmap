namespace RoadmapBlazor.Models;

public class ComentarioModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public string ComentarioHtmlClass { get; set; } = string.Empty;
    public int ComentarioTotalVotes { get; set; }

    public Guid? UserId { get; set; }
    public Guid RoadmapId { get; set; }
}
