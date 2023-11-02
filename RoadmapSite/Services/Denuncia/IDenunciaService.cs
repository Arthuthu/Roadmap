﻿using RoadmapSite.Models;

namespace RoadmapSite.Services.Denuncia;

public interface IDenunciaService
{
    Task<IList<DenunciaModel>?> GetAllDenuncias();
    Task<string?> CreateDenuncia(DenunciaModel denuncia);
    Task<string?> DeleteDenuncia(Guid id);
}