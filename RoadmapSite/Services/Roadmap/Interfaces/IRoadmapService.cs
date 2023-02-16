﻿using RoadmapSite.Models;

namespace RoadmapSite.Services.Roadmap.Interfaces;

public interface IRoadmapService
{
	Task<string> CreateRoadmap(RoadmapClassModel roadmap);
	Task<IList<RoadmapClassModel>> GetAllRoadmaps();
	Task<IList<RoadmapClassModel>> GetRoadmapByUserId(Guid userId);
	Task<RoadmapClassModel> GetRoadmapById(Guid id);
}
