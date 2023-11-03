using Roadmap.Domain.Models;

namespace Roadmap.Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task CreateComentario(ComentarioModel Comentario);
        Task DeleteComentario(Guid id);
        Task DeleteAllUserComentarios(Guid userId);
        Task<IEnumerable<ComentarioModel>> GetAllComentarios(Guid roadmapId);
        Task<ComentarioModel?> GetComentarioById(Guid id);
        Task UpdateComentario(ComentarioModel Comentario);
    }
}