using Roadmap.Domain.Models;

namespace Roadmap.Domain.Interfaces
{
    public interface IRoadmapVotesRepository
    {
        Task AddRoadmapVote(Guid roadmapVoteId, Guid userId, Guid roadmapId);
        Task DeleteRoadmapVote(Guid id);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotesByUserId(Guid userId);
        Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes(Guid userId, Guid roadmapId);
    }
}