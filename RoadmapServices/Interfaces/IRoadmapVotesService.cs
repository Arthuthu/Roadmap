using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapVotesService
    {
        Task<string> AddRoadmapVote(RoadmapVotesModel roadmapVote);
		Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid userId);
        Task<RoadmapVotesModel> GetRoadmapVotedIdByUserAndRoadmapId(Guid userId, Guid roadmapId);

	}
}