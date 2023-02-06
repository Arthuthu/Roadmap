using RoadmapSite.Models;

namespace RoadmapSite.Services.Roadmap.Interfaces;

public interface IRoadmapService
{
	Task<string> CreateRoadmap(RoadmapClassModel roadmap);
	Task<IList<RoadmapClassModel>> GetAllRoadmaps();

}
