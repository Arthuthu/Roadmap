using RoadmapSite.Models;

namespace RoadmapSite.Services.Comentario.Interfaces
{
    public interface IComentarioService
    {
        Task<string> CreateComentario(ComentarioModel comentario);
        Task<string> DeleteComentario(Guid id);
        Task<IList<ComentarioModel>?> GetAllComentarios();
        Task<string> UpdateComentario(ComentarioModel comentario);
    }
}