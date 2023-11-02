using Domain.Interfaces;
using Domain.Models;
using Domain.SqlDataAccess;

namespace Domain.Repositories;

public class RoadmapVotesRepository : IRoadmapVotesRepository
{
	private readonly ISqlDataAccess _db;

	public RoadmapVotesRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotes(Guid userId, Guid roadmapId)
	{
		return _db.LoadData<RoadmapVotesModel, dynamic>("dbo.spRoadmapVotes_GetAll",
			new { UserId = userId, RoadmapId = roadmapId });
	}

	public Task<IEnumerable<RoadmapVotesModel>> GetAllRoadmapVotesByUserId(Guid userId)
	{
		return _db.LoadData<RoadmapVotesModel, dynamic>("dbo.spRoadmapVotes_GetAllByUserId",
			new { UserId = userId });
	}

	public Task AddRoadmapVote(Guid Id, Guid userId, Guid roadmapId)
	{
		return _db.SaveData("dbo.spRoadmapVotes_Add", new
		{
			Id,
			userId,
			roadmapId
		});
	}
	public Task DeleteRoadmapVote(Guid id)
	{
		return _db.SaveData("dbo.spRoadmapVotes_Delete", new { Id = id });
	}
}
