using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IComentarioRepository
    {
        Task AddComentario(ComentarioModel Comentario);
        Task DeleteComentario(Guid id);
        Task<IEnumerable<ComentarioModel>> GetAllComentarios();
        Task<ComentarioModel?> GetComentarioById(Guid id);
        Task UpdateComentario(ComentarioModel Comentario);
    }
}