using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IRoadmapVotesRepository
    {
        Task AddRoadmapVote(RoadmapVotesModel roadmapVotes);
        Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid userId);
		Task<RoadmapVotesModel> GetRoadmapVotedIdByUserAndRoadmapId(Guid userId, Guid roadmapId);
        Task<IList<RoadmapVotesModel>> GetRoadmapVotesByRoadmapId(Guid roadmapId);
	}
}