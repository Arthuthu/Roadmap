using RoadmapSite.Models;

namespace RoadmapSite.Services.RoadmapVotes.Interfaces
{
    public interface IRoadmapVotesService
    {
        Task<string?> AddRoadmapVote(RoadmapVotesModel roadmapVote);
        Task<IList<RoadmapVotesModel>?> GetAllRoadmaps();
        Task<IList<RoadmapVotesModel>?> GetAllRoadmapsUserVoted(Guid userId);
		Task<RoadmapVotesModel?> GetRoadmapVoteIdByUserAndRoadmapId(Guid userId, Guid roadmapId);
		Task<IList<RoadmapVotesModel>?> GetRoadmapVotesByRoadmapId(Guid roadmapId);
        Task<string?> RemoveRoadmapVote(Guid roadmapVoteId);

	}
}