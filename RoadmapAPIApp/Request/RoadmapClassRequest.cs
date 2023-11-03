namespace Roadmap.API.Request;

public class RoadmapClassRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Category { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public string? AuthorName { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}
