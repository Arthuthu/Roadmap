using Domain.Models;

namespace Domain.Interfaces
{
	public interface IComentarioVotesRepository
	{
		Task AddComentarioVote(Guid Id, Guid userId, Guid comentarioId);
		Task DeleteComentarioVote(Guid id);
		Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes(Guid userId, Guid comentarioId);
	}
}