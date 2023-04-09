using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapVotesService
    {
        Task<string> AddRoadmapVote(Guid userId, Guid roadmapId);
		Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotesByUserId(Guid userId);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
	}
}