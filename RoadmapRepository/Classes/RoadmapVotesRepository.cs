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
