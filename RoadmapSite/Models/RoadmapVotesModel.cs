namespace RoadmapBlazor.Models;

public class RoadmapVotesModel
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid RoadmapId { get; set; }
}
