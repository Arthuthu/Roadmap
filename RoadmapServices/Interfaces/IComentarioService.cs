using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IComentarioService
    {
        Task CreateComentario(ComentarioModel comentario);
        Task DeleteComentario(Guid id);
        Task<IEnumerable<ComentarioModel>> GetAllComentarios();
        Task<ComentarioModel?> GetComentarioById(Guid id);
        Task UpdateComentario(ComentarioModel comentario);
    }
}