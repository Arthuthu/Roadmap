using RoadmapSite.Models;

namespace RoadmapSite.Services.RoadmapVotes.Interfaces
{
    public interface IRoadmapVotesService
    {
		Task<string?> AddRoadmapVote(Guid? userId, Guid roadmapId);
		Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotes();
        Task<string?> RemoveRoadmapVote(Guid roadmapVoteId);

	}
}