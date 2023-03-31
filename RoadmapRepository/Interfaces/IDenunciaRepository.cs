using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IDenunciaRepository
    {
        Task CreateDenuncia(DenunciaModel denuncia);
        Task DeleteDenuncia(Guid id);
        Task<IEnumerable<DenunciaModel>> GetAllDenuncias();
        Task<DenunciaModel?> GetDenunciaById(Guid id);
        Task UpdateDenuncia(DenunciaModel denuncia);
    }
}