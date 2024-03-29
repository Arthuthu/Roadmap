﻿using Roadmap.Domain.Interfaces;
using Roadmap.Domain.Models;
using Roadmap.Domain.SqlDataAccess;

namespace Roadmap.Domain.Repositories;

public class RoadmapClassRepository : IRoadmapClassRepository
{
    private readonly ISqlDataAccess _db;

    public RoadmapClassRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps()
    {
        return _db.LoadData<RoadmapClassModel, dynamic>("dbo.spRoadmap_GetAll", new { });
    }

    public Task<IEnumerable<RoadmapClassModel>> GetAllApprovedRoadmaps()
    {
        return _db.LoadData<RoadmapClassModel, dynamic>("dbo.spRoadmap_GetAllByApproved", new { });
    }

    public Task<IEnumerable<RoadmapClassModel>> GetAllNotApprovedRoadmaps()
    {
        return _db.LoadData<RoadmapClassModel, dynamic>("dbo.spRoadmap_GetAllByNotApproved", new { });
    }

    public Task<IEnumerable<RoadmapClassModel>> GetAllApprovedRoadmapsByCategory()
    {
        return _db.LoadData<RoadmapClassModel, dynamic>("dbo.spRoadmap_GetAllApprovedByCategory", new { });
    }

    public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
    {
        var results = await _db.LoadData<RoadmapClassModel, dynamic>(
            "dbo.spRoadmap_GetById",
            new { Id = id });

        return results.FirstOrDefault();
    }

    public async Task<IList<RoadmapClassModel>> GetRoadmapsByUserId(Guid userId)
    {
        var results = await _db.LoadData<RoadmapClassModel, dynamic>(
            "dbo.spRoadmap_GetRoadmapsByUserId",
            new { UserId = userId });

        return results.ToList();
    }

    public Task AddRoadmap(RoadmapClassModel roadmap)
    {
        return _db.SaveData("dbo.spRoadmap_Add", new
        {
            roadmap.Id,
            roadmap.Name,
            roadmap.Description,
            roadmap.Category,
            roadmap.IsApproved,
            roadmap.AuthorName,
            roadmap.UserId,
            roadmap.CreatedDate
        });
    }

    public Task UpdateRoadmap(RoadmapClassModel roadmap)
    {
        return _db.SaveData("dbo.spRoadmap_Update", new
        {
            roadmap.Id,
            roadmap.Name,
            roadmap.Description,
            roadmap.Category,
            roadmap.IsApproved,
            roadmap.UpdatedDate,
            roadmap.UserId
        });
    }

    public Task DeleteAllUserRoadmaps(Guid userId)
    {
        return _db.SaveData("dbo.spRoadmap_DeleteAllUserRoadmaps", new { UserId = userId });
    }

    public Task DeleteRoadmap(Guid id)
    {
        return _db.SaveData("dbo.spRoadmap_Delete", new { Id = id });
    }
}
