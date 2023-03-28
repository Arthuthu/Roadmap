using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Classes;

public class ComentarioVotesService : IComentarioVotesService
{
	private readonly IComentarioVotesRepository _comentarioVotesRepository;
	private readonly IMessageHandler _messageHandler;
	private string comentarioVotingResponseMessage = "";

	public ComentarioVotesService(IComentarioVotesRepository comentarioVotesRepository,
		IMessageHandler messageHandler)
	{
		_comentarioVotesRepository = comentarioVotesRepository;
		_messageHandler = messageHandler;
	}

	public Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes()
	{
		return _comentarioVotesRepository.GetAllComentarioVotes();
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
