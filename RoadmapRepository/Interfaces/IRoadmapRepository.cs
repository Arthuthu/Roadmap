using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
	public interface IRoadmapRepository
	{
		Task<IEnumerable<RoadmapModel>> GetAllRoadmaps();
		Task<RoadmapModel?> GetRoadmapById(Guid id);
		Task AddRoadmap(RoadmapModel roadmap);
		Task UpdateRoadmap(RoadmapModel roadmap);
		Task DeleteRoadmap(Guid id);
	}
}