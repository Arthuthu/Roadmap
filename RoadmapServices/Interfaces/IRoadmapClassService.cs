using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IRoadmapClassService
    {
		Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps();
		Task<RoadmapClassModel?> GetRoadmapById(Guid id);
		Task<IList<RoadmapClassModel>> GetRoadmapsByUserId(Guid userId);
		Task<IList<string>> AddRoadmap(RoadmapClassModel roadmap);
		Task UpdateRoadmap(RoadmapClassModel roadmap);
        Task DeleteAllUserRoadmaps(Guid userId);
        Task DeleteRoadmap(Guid id);
    }
}