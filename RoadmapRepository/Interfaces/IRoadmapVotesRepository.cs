using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IRoadmapVotesRepository
    {
        Task AddRoadmapVote(RoadmapVotesModel roadmapVotes);
        Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes();
    }
}