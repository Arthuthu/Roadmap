using Roadmap.Domain.Models;

namespace Roadmap.Infra.Interfaces
{
    public interface IDenunciaService
    {
        Task CreateDenuncia(DenunciaModel denuncia);
        Task DeleteDenuncia(Guid id);
        Task<IEnumerable<DenunciaModel>> GetAllDenuncias();
        Task<DenunciaModel?> GetDenunciaById(Guid id);
        Task UpdateDenuncia(DenunciaModel denuncia);
    }
}