using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Classes;

public class ComentarioVotesService : IComentarioVotesService
{
	private readonly IComentarioVotesRepository _comentarioVotesRepository;
	private string comentarioVotingResponseMessage = "";

	public ComentarioVotesService(IComentarioVotesRepository comentarioVotesRepository)
	{
		_comentarioVotesRepository = comentarioVotesRepository;
	}

	public Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes(Guid userId, Guid comentarioId)
	{
		return _comentarioVotesRepository.GetAllComentarioVotes(userId, comentarioId);
	}

    public async Task<string> AddComentarioVote(Guid userId, Guid comentarioId)
	{
		Guid comentarioVoteId = Guid.NewGuid();

		try
		{
			await _comentarioVotesRepository.AddComentarioVote(comentarioVoteId, userId, comentarioId);
		}
		catch
		{
			comentarioVotingResponseMessage = "Ocorreu um erro ao adicionar o voto";
			return comentarioVotingResponseMessage;
		}

		comentarioVotingResponseMessage = "Voto adicionado com sucesso";
		return comentarioVotingResponseMessage;
	}

	public Task DeleteComentarioVote(Guid id)
	{
		return _comentarioVotesRepository.DeleteComentarioVote(id);
	}
}
