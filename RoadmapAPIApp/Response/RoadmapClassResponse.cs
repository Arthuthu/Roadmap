namespace Roadmap.API.Response;

public class RoadmapClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Category { get; set; }
    public bool IsApproved { get; set; }
    public string? AuthorName { get; set; }


    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }


    public Guid UserId { get; set; }
}
