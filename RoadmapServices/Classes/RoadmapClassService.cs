using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapServices.Classes;

public class RoadmapClassService : IRoadmapClassService
{
	private readonly IRoadmapClassRepository _roadmapData;

	public RoadmapClassService(IRoadmapClassRepository roadmapData)
	{
		_roadmapData = roadmapData;
	}

	public Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps()
	{
		return _roadmapData.GetAllRoadmaps();
	}

	public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
	{
		return await _roadmapData.GetRoadmapById(id);
	}

	public async Task AddRoadmap(RoadmapClassModel roadmap)
	{
		await _roadmapData.AddRoadmap(roadmap);
	}

	public Task UpdateRoadmap(RoadmapClassModel roadmap)
	{
		return _roadmapData.UpdateRoadmap(roadmap);
	}

	public Task DeleteRoadmap(Guid id)
	{
		return _roadmapData.DeleteRoadmap(id);
	}
}
