using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Classes;

public class RoadmapVotesService : IRoadmapVotesService
{
	private readonly IRoadmapVotesRepository _roadmapVotesRepository;
	private string roadmapVotingResponseMessage = "";

	public RoadmapVotesService(IRoadmapVotesRepository roadmapVotesRepository)
	{
		_roadmapVotesRepository = roadmapVotesRepository;
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes(Guid userId, Guid roadmapId)
	{
		return _roadmapVotesRepository.GetAllRoadmapVotes(userId, roadmapId);
	}

    public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotesByUserId(Guid userId)
    {
        return _roadmapVotesRepository.GetAllRoadmapVotesByUserId(userId);
    }

    public async Task<string> AddRoadmapVote(Guid userId, Guid roadmapId)
	{
		Guid roadmapVoteId = Guid.NewGuid();

		try
		{
			await _roadmapVotesRepository.AddRoadmapVote(roadmapVoteId, userId, roadmapId);
		}
		catch
		{
			roadmapVotingResponseMessage = "Ocorreu um erro ao adicionar o voto";
			return roadmapVotingResponseMessage;
		}

		roadmapVotingResponseMessage = "Voto adicionado com sucesso";
		return roadmapVotingResponseMessage;
	}

	public Task DeleteRoadmapVote(Guid id)
	{
		return _roadmapVotesRepository.DeleteRoadmapVote(id);
	}
}
