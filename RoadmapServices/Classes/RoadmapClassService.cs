using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Classes;

public class RoadmapClassService : IRoadmapClassService
{
	private readonly IRoadmapClassRepository _roadmapRepository;
	private readonly IMessageHandler _messageHandler;

	public RoadmapClassService(IRoadmapClassRepository roadmapRepository,
		IMessageHandler messageHandler)
	{
		_roadmapRepository = roadmapRepository;
		_messageHandler = messageHandler;
	}

	public Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps()
	{
		return _roadmapRepository.GetAllRoadmaps();
	}

	public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
	{
		return await _roadmapRepository.GetRoadmapById(id);
	}

	public async Task<IList<RoadmapClassModel>> GetRoadmapByUserId(Guid userId)
	{
		var results = await _roadmapRepository.GetRoadmapByUserId(userId);

		if (results is null)
		{
			throw new Exception("Usuario não tem roadmaps criados");
		}

		return results;
	}

	public async Task<IList<string>> AddRoadmap(RoadmapClassModel roadmap)
	{
		IList<string> registrationMessages = new List<string>();

		registrationMessages =  _messageHandler.ValidateRoadmapCreation(roadmap);

		if (registrationMessages.Count != 0)
		{
			return registrationMessages;
		}

		roadmap.Id = Guid.NewGuid();
		roadmap.IsApproved = false;
		roadmap.CreatedDate = DateTime.UtcNow.AddHours(-3);

		try
		{
			await _roadmapRepository.AddRoadmap(roadmap);
			registrationMessages.Add("Roadmap criado com sucesso");
		}
		catch (Exception ex)
		{
			registrationMessages.Add($"Ocorreu um erro durante a criação do roadmap: {ex.Message}");
		}

		return registrationMessages;
	}

	public Task UpdateRoadmap(RoadmapClassModel roadmap)
	{
		roadmap.UpdatedDate = DateTime.UtcNow.AddHours(-3);
		return _roadmapRepository.UpdateRoadmap(roadmap);
	}
    public Task DeleteAllUserRoadmaps(Guid userId)
    {
        return _roadmapRepository.DeleteAllUserRoadmaps(userId);
    }

    public Task DeleteRoadmap(Guid id)
	{
		return _roadmapRepository.DeleteRoadmap(id);
	}
}
