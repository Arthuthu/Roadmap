using RoadmapSite.Models;

namespace RoadmapSite.Services.ComentarioVotes.Interfaces
{
    public interface IComentarioVotesService
    {
        Task<string?> AddComentarioVote(Guid? userId, Guid comentarioId);
        Task<IList<ComentarioVotesModel>?> GetAllComentarioVotes(Guid id, Guid? userid);
        Task<string?> RemoveComentarioVote(Guid comentarioVoteId);
    }
}