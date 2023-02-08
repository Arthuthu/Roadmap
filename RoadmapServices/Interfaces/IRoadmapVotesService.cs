using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapVotesService
    {
        Task<string> AddRoadmapVote(RoadmapVotesModel roadmapVote);
		Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
    }
}