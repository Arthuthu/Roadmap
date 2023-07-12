using RoadmapSite.Models;

namespace RoadmapSite.Services.ComentarioVotes.Interfaces
{
    public interface IComentarioVotesService
    {
		Task<IList<ComentarioVotesModel>?> GetAllComentarioVotes(Guid? userId, Guid comentarioId);
		Task<string?> AddComentarioVote(Guid? userId, Guid comentarioId);
        Task<string?> RemoveComentarioVote(Guid comentarioVoteId);
    }
}