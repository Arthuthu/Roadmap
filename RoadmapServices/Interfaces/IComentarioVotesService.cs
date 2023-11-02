using Domain.Models;

namespace Infra.Interfaces
{
	public interface IComentarioVotesService
	{
		Task<string> AddComentarioVote(Guid userId, Guid comentarioId);
		Task DeleteComentarioVote(Guid id);
		Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes(Guid userId, Guid comentarioId);
	}
}