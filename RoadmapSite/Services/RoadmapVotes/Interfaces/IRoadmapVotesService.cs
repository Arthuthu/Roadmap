using RoadmapSite.Models;

namespace RoadmapSite.Services.RoadmapVotes.Interfaces
{
    public interface IRoadmapVotesService
    {
        Task<string> AddRoadmapVote(RoadmapVotesModel roadmapVote);
        Task<IList<RoadmapVotesModel>> GetAllRoadmaps();
        Task<string> RemoveRoadmapVote(Guid roadmapVoteId);
        Task<IList<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid id);
		Task<RoadmapVotesModel> GetRoadmapVotedIdByUserAndRoadmapId(Guid userId, Guid roadmapId);
        Task<IList<RoadmapVotesModel>> GetRoadmapVotesByRoadmapId(Guid roadmapId);
	}
}