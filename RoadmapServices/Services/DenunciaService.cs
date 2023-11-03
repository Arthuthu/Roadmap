using Roadmap.Domain.Interfaces;
using Roadmap.Domain.Models;
using Roadmap.Infra.Interfaces;

namespace Roadmap.Infra.Services;

public class DenunciaService : IDenunciaService
{
    private readonly IDenunciaRepository _denunciaRepository;

    public DenunciaService(IDenunciaRepository denunciaRepository)
    {
        _denunciaRepository = denunciaRepository;
    }

    public Task<IEnumerable<DenunciaModel>> GetAllDenuncias()
    {
        return _denunciaRepository.GetAllDenuncias();
    }

    public async Task<DenunciaModel?> GetDenunciaById(Guid id)
    {
        return await _denunciaRepository.GetDenunciaById(id);
    }

    public async Task CreateDenuncia(DenunciaModel denuncia)
    {
        denuncia.Id = Guid.NewGuid();
        denuncia.CreatedDate = DateTime.UtcNow.AddHours(-3);
        await _denunciaRepository.CreateDenuncia(denuncia);
    }

    public Task UpdateDenuncia(DenunciaModel denuncia)
    {
        denuncia.UpdatedDate = DateTime.UtcNow.AddHours(-3);
        return _denunciaRepository.UpdateDenuncia(denuncia);
    }

    public Task DeleteDenuncia(Guid id)
    {
        return _denunciaRepository.DeleteDenuncia(id);
    }
}
