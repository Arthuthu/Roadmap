namespace Roadmap.Domain.Models;

public class RoadmapClassModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Category { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public string? AuthorName { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }


    public Guid UserId { get; set; }
}
