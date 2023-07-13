using RoadmapSite.Models;

namespace RoadmapSite.Services.Roadmap.Interfaces;

public interface IRoadmapService
{
	Task<IList<RoadmapClassModel>?> GetAllRoadmaps();
	Task<IList<RoadmapClassModel>?> GetAllApprovedRoadmaps();
	Task<IList<RoadmapClassModel>?> GetAllNotApprovedRoadmaps();
	Task<IList<RoadmapClassModel>?> GetAllApprovedRoadmapsByCategory();
	Task<IList<RoadmapClassModel>?> GetRoadmapByUserId(Guid userId);
	Task<RoadmapClassModel?> GetRoadmapById(Guid id);
	Task<string?> CreateRoadmap(RoadmapClassModel roadmap);
	Task<string?> UpdateRoadmap(RoadmapClassModel roadmap);
	Task<string?> DeleteRoadmap(Guid id);
	Task<string?> DeleteAllUserRoadmaps(Guid userId);
}
