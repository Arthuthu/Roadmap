using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class RoadmapRepository
{
	private readonly ISqlDataAccess _db;

	public RoadmapRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<RoadmapModel>> GetAllRoadmaps()
	{
		return _db.LoadData<RoadmapModel, dynamic>("dbo.spRoadmap_GetAll", new { });
	}

	public async Task<RoadmapModel?> GetRoadmapById(Guid id)
	{
		var results = await _db.LoadData<RoadmapModel, dynamic>(
			"dbo.spRoadmap_GetById",
			new { Id = id });

		return results.FirstOrDefault();
	}

	public Task AddRoadmap(RoadmapModel roadmap)
	{
		return _db.SaveData("dbo.spRoadmap_Add", new { roadmap.Id,
			roadmap.Name,
			roadmap.Description,
			roadmap.UserId});
	}

	public Task UpdateRoadmap(RoadmapModel roadmap)
	{
		return _db.SaveData("dbo.spRoadmap_Update", roadmap);
	}

	public Task DeleteRoadmap(Guid id)
	{
		return _db.SaveData("dbo.spRoadmap_Delete", new { Id = id });
	}
}
