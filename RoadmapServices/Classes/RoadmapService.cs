using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;

namespace RoadmapServices.Classes;

public class RoadmapService : IRoadmapService
{
	private readonly IRoadmapRepository _roadmapData;

	public RoadmapService(IRoadmapRepository roadmapData)
	{
		_roadmapData = roadmapData;
	}

	public Task<IEnumerable<RoadmapModel>> GetAllRoadmaps()
	{
		return _roadmapData.GetAllRoadmaps();
	}

	public async Task<RoadmapModel?> GetRoadmapById(Guid id)
	{
		return await _roadmapData.GetRoadmapById(id);
	}

	public async Task AddRoadmap(RoadmapModel roadmap)
	{
		await _roadmapData.AddRoadmap(roadmap);
	}

	public Task UpdateRoadmap(RoadmapModel roadmap)
	{
		return _roadmapData.UpdateRoadmap(roadmap);
	}

	public Task DeleteRoadmap(Guid id)
	{
		return _roadmapData.DeleteRoadmap(id);
	}
}
