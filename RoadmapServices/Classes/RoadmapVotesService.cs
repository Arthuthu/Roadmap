using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Classes;

public class RoadmapVotesService : IRoadmapVotesService
{
	private readonly IRoadmapVotesRepository _roadmapVotesRepository;
	private readonly IMessageHandler _messageHandler;
	private string roadmapVotingResponseMessage = "";

	public RoadmapVotesService(IRoadmapVotesRepository roadmapVotesRepository,
		IMessageHandler messageHandler)
	{
		_roadmapVotesRepository = roadmapVotesRepository;
		_messageHandler = messageHandler;
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes()
	{
		return _roadmapVotesRepository.GetAllRoadmapVotes();
	}

	public async Task<string> AddRoadmapVote(RoadmapVotesModel roadmapVote)
	{
		roadmapVote.Id = Guid.NewGuid();

		try
		{
			await _roadmapVotesRepository.AddRoadmapVote(roadmapVote);
		}
		catch
		{
			roadmapVotingResponseMessage = "Ocorreu um erro ao adicionar o voto";
			return roadmapVotingResponseMessage;
		}

		roadmapVotingResponseMessage = "Voto adicionado com sucesso";
		return roadmapVotingResponseMessage;
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid userId)
	{
		return _roadmapVotesRepository.GetAllRoadmapsUserVoted(userId);
	}

	public Task<RoadmapVotesModel> GetRoadmapVotedIdByUserAndRoadmapId(Guid userId, Guid roadmapId)
	{
		return _roadmapVotesRepository.GetRoadmapVotedIdByUserAndRoadmapId(userId, roadmapId);
	}

	public Task DeleteRoadmapVote(Guid id)
	{
		return _roadmapVotesRepository.DeleteRoadmapVote(id);
	}
}
