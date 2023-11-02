using Domain.Interfaces;
using Domain.Models;
using Domain.SqlDataAccess;

namespace Domain.Repositories;

public class ComentarioRepository : IComentarioRepository
{
	private readonly ISqlDataAccess _db;

	public ComentarioRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<ComentarioModel>> GetAllComentarios(Guid roadmapId)
	{
		return _db.LoadData<ComentarioModel, dynamic>("dbo.spComentario_GetAllComentariosByRoadmapId",
			new { RoadmapId = roadmapId });
	}

	public async Task<ComentarioModel?> GetComentarioById(Guid id)
	{
		var results = await _db.LoadData<ComentarioModel, dynamic>(
			"dbo.spComentario_GetById",
			new { Id = id });

		return results.FirstOrDefault();
	}

	public Task CreateComentario(ComentarioModel comentario)
	{
		return _db.SaveData("dbo.spComentario_Add", new
		{
			comentario.Id,
			comentario.Description,
			comentario.AuthorName,
			comentario.CreatedDate,
			comentario.UserId,
			comentario.RoadmapId
		});
	}

	public Task UpdateComentario(ComentarioModel comentario)
	{
		return _db.SaveData("dbo.spComentario_Update", comentario);
	}
	public Task DeleteAllUserComentarios(Guid userId)
	{
		return _db.SaveData("dbo.spComentario_DeleteAllUserComments", new { UserId = userId });
	}

	public Task DeleteComentario(Guid id)
	{
		return _db.SaveData("dbo.spComentario_Delete", new { Id = id });
	}
}
