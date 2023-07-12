using RoadmapSite.Models;

namespace RoadmapSite.Services.Comentario.Interfaces
{
    public interface IComentarioService
    {
        Task<string?> CreateComentario(ComentarioModel comentario);
        Task<string?> DeleteComentario(Guid id);
        Task<string?> DeleteAllUserComentarios(Guid userId);
        Task<IList<ComentarioModel>?> GetAllComentariosByRoadmapId(Guid roadmapId);
        Task<ComentarioModel?> GetComentarioById(Guid? comentarioId);
		Task<string?> UpdateComentario(ComentarioModel comentario);
    }
}