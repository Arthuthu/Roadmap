using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class RoadmapVotesRepository : IRoadmapVotesRepository
{
	private readonly ISqlDataAccess _db;

	public RoadmapVotesRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes()
	{
		return _db.LoadData<RoadmapVotesModel, dynamic>("dbo.spRoadmapVotes_GetAll", new { });
	}

	public Task AddRoadmapVote(RoadmapVotesModel roadmapVotes)
	{
		return _db.SaveData("dbo.spRoadmapVotes_Add", new
		{
			roadmapVotes.Id,
			roadmapVotes.UserId,
			roadmapVotes.RoadmapId
		});
	}
	public Task DeleteRoadmapVote(Guid id)
	{
		return _db.SaveData("dbo.spRoadmapVotes_Delete", new { Id = id });
	}

	public async Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid userId)
	{
		return await _db.LoadData<RoadmapVotesModel, dynamic>(
		"dbo.spRoadmapVotes_GetAllRoadmapsUserVoted",
		new { UserId = userId });
	}

	public async Task<RoadmapVotesModel> GetRoadmapVotedIdByUserAndRoadmapId(Guid userId, Guid roadmapId)
	{
		var results =  await _db.LoadData<RoadmapVotesModel, dynamic>(
		"dbo.spRoadmapVotes_GetRoadmapVotedIdByUserAndRoadmapId",
		new { UserId = userId, RoadmapId = roadmapId });
		
		return results.FirstOrDefault();
	}

}
