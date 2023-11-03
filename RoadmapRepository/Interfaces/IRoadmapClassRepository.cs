using Roadmap.Domain.Models;

namespace Roadmap.Domain.Interfaces
{
    public interface IRoadmapClassRepository
    {
        Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps();
        Task<IEnumerable<RoadmapClassModel>> GetAllApprovedRoadmaps();
        Task<IEnumerable<RoadmapClassModel>> GetAllNotApprovedRoadmaps();
        Task<IEnumerable<RoadmapClassModel>> GetAllApprovedRoadmapsByCategory();
        Task<RoadmapClassModel?> GetRoadmapById(Guid id);
        Task<IList<RoadmapClassModel>> GetRoadmapsByUserId(Guid userId);
        Task AddRoadmap(RoadmapClassModel roadmap);
        Task UpdateRoadmap(RoadmapClassModel roadmap);
        Task DeleteRoadmap(Guid id);
        Task DeleteAllUserRoadmaps(Guid userId);
    }
}