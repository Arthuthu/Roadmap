using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class ComentarioRepository : IComentarioRepository
{
	private readonly ISqlDataAccess _db;

	public ComentarioRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<ComentarioModel>> GetAllComentarios()
	{
		return _db.LoadData<ComentarioModel, dynamic>("dbo.spComentario_GetAll", new { });
	}

	public async Task<ComentarioModel?> GetComentarioById(Guid id)
	{
		var results = await _db.LoadData<ComentarioModel, dynamic>(
			"dbo.spComentario_GetById",
			new { Id = id });

		return results.FirstOrDefault();
	}

	public Task AddComentario(ComentarioModel Comentario)
	{
		return _db.SaveData("dbo.spComentario_Add", new
		{
			Comentario.Id,
			Comentario.Comentario,
			Comentario.UserId
		});
	}

	public Task UpdateComentario(ComentarioModel Comentario)
	{
		return _db.SaveData("dbo.spComentario_Update", Comentario);
	}

	public Task DeleteComentario(Guid id)
	{
		return _db.SaveData("dbo.spComentario_Delete", new { Id = id });
	}
}
