using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IRoadmapVotesRepository
    {
        Task AddRoadmapVote(Guid roadmapVoteId, Guid userId, Guid roadmapId);
		Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
	}
}