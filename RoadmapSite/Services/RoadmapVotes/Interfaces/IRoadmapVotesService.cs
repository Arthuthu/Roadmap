using RoadmapSite.Models;

namespace RoadmapSite.Services.RoadmapVotes.Interfaces
{
    public interface IRoadmapVotesService
    {
		Task<string?> AddRoadmapVote(Guid? userId, Guid roadmapId);
		Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotes(Guid? userId, Guid roadmapId);
        Task<string?> RemoveRoadmapVote(Guid roadmapVoteId);
        Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotesByUserId(Guid? userId);

    }
}