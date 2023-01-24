using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapClassService
    {
		Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps();
		Task<RoadmapClassModel?> GetRoadmapById(Guid id);
		Task AddRoadmap(RoadmapClassModel roadmap);
		Task UpdateRoadmap(RoadmapClassModel roadmap);
		Task DeleteRoadmap(Guid id);
    }
}