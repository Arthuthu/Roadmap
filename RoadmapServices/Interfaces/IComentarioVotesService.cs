using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IComentarioVotesService
    {
        Task<string> AddComentarioVote(Guid userId, Guid comentarioId);
        Task DeleteComentarioVote(Guid id);
        Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes(Guid id, Guid userId);
    }
}