using Domain.Models;

namespace Infra.Interfaces
{
	public interface IComentarioService
	{
		Task CreateComentario(ComentarioModel comentario);
		Task DeleteComentario(Guid id);
		Task DeleteAllUserComentarios(Guid userId);
		Task<IEnumerable<ComentarioModel>> GetAllComentarios(Guid roadmapId);
		Task<ComentarioModel?> GetComentarioById(Guid id);
		Task UpdateComentario(ComentarioModel comentario);
	}
}