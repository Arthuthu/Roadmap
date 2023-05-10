using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class ComentarioVotesRepository : IComentarioVotesRepository
{
	private readonly ISqlDataAccess _db;

	public ComentarioVotesRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<ComentarioVotesModel>> GetAllComentarioVotes(Guid userId, Guid comentarioId)
	{
		return _db.LoadData<ComentarioVotesModel, dynamic>("dbo.spComentarioVotes_GetAll", 
			new { UserId = userId, ComentarioId = comentarioId });
	}

    public Task AddComentarioVote(Guid Id, Guid userId, Guid comentarioId)
	{
		return _db.SaveData("dbo.spComentarioVotes_Add", new
		{
			Id,
			userId,
			comentarioId
		});
	}
	public Task DeleteComentarioVote(Guid id)
	{
		return _db.SaveData("dbo.spComentarioVotes_Delete", new { Id = id });
	}
}
