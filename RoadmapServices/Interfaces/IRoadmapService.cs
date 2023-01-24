using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapService
    {
		Task<IEnumerable<RoadmapModel>> GetAllRoadmaps();
		Task<RoadmapModel?> GetRoadmapById(Guid id);
		Task AddRoadmap(RoadmapModel roadmap);
		Task UpdateRoadmap(RoadmapModel roadmap);
		Task DeleteRoadmap(Guid id);
    }
}