namespace Roadmap.API.Request;

public class ComentarioRequest
{
    public string? Description { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid RoadmapId { get; set; }

}

