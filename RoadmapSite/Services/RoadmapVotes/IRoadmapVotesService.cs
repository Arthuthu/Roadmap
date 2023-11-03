using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.RoadmapVotes;

public interface IRoadmapVotesService
{
    Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotes(Guid? userId, Guid roadmapId);
    Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotesByUserId(Guid? userId);
    Task<string?> AddRoadmapVote(Guid? userId, Guid roadmapId);
    Task<string?> RemoveRoadmapVote(Guid roadmapVoteId);
}