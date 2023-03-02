using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

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

	public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
	{
		var results = await _db.LoadData<RoadmapClassModel, dynamic>(
			"dbo.spRoadmap_GetById",
			new { Id = id });

		return results.FirstOrDefault();
	}

	public async Task<IList<RoadmapClassModel>> GetRoadmapByUserId(Guid userId)
	{
		var results = await _db.LoadData<RoadmapClassModel, dynamic>(
			"dbo.spRoadmap_GetRoadmapsByUserId",
			new { UserId = userId });

		return results.ToList();
	}

	public async Task<IList<RoadmapClassModel>> GetRoadmapsByCategory(string category)
	{
		var results = await _db.LoadData<RoadmapClassModel, dynamic>(
			"dbo.spRoadmap_GetRoadmapsByCategory",
			new { Category = category });

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
			roadmap.UserId,
			roadmap.CreatedDate
		});
	}

	public Task UpdateRoadmap(RoadmapClassModel roadmap)
	{
		return _db.SaveData("dbo.spRoadmap_Update", roadmap);
	}

	public Task DeleteRoadmap(Guid id)
	{
		return _db.SaveData("dbo.spRoadmap_Delete", new { Id = id });
	}
}
