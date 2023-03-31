using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class DenunciaRepository : IDenunciaRepository
{
    private readonly ISqlDataAccess _db;

    public DenunciaRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<DenunciaModel>> GetAllDenuncias()
    {
        return _db.LoadData<DenunciaModel, dynamic>("dbo.spDenuncia_GetAll", new { });
    }

    public async Task<DenunciaModel?> GetDenunciaById(Guid id)
    {
        var results = await _db.LoadData<DenunciaModel, dynamic>(
            "dbo.spDenuncia_GetById",
            new { Id = id });

        return results.FirstOrDefault();
    }

    public Task CreateDenuncia(DenunciaModel denuncia)
    {
        return _db.SaveData("dbo.spDenuncia_Add", new
        {
            denuncia.Id,
            denuncia.Description,
            denuncia.Type,
            denuncia.AuthorId,
            denuncia.UserId,
            denuncia.RoadmapId,
            denuncia.CommentId,
            denuncia.CreatedDate
        });
    }

    public Task UpdateDenuncia(DenunciaModel denuncia)
    {
        return _db.SaveData("dbo.spDenuncia_Update", denuncia);
    }

    public Task DeleteDenuncia(Guid id)
    {
        return _db.SaveData("dbo.spDenuncia_Delete", new { Id = id });
    }
}
