using Site.Models;

namespace Site.Services.Denuncia;

public interface IDenunciaService
{
	Task<IList<DenunciaModel>?> GetAllDenuncias();
	Task<string?> CreateDenuncia(DenunciaModel denuncia);
	Task<string?> DeleteDenuncia(Guid id);
}