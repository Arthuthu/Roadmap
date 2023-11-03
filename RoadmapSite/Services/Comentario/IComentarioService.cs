using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.Comentario;

public interface IComentarioService
{
    Task<IList<ComentarioModel>?> GetAllComentariosByRoadmapId(Guid roadmapId);
    Task<ComentarioModel?> GetComentarioById(Guid? comentarioId);
    Task<string?> CreateComentario(ComentarioModel comentario);
    Task<string?> UpdateComentario(ComentarioModel comentario);
    Task<string?> DeleteComentario(Guid id);
    Task<string?> DeleteAllUserComentarios(Guid userId);
}