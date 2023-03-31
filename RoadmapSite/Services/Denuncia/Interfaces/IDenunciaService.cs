using RoadmapSite.Models;

namespace RoadmapSite.Services.Denuncia.Interfaces
{
    public interface IDenunciaService
    {
        Task<string> CreateDenuncia(DenunciaModel denuncia);
        Task<string> DeleteDenuncia(Guid id);
        Task<IList<DenunciaModel>?> GetAllDenuncias();
    }
}