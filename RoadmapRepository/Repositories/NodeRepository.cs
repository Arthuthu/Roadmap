using Roadmap.Domain.Interfaces;
using Roadmap.Domain.Models;
using Roadmap.Domain.SqlDataAccess;

namespace Roadmap.Domain.Repositories;

public class NodeRepository : INodeRepository
{
    private readonly ISqlDataAccess _db;

    public NodeRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<NodeModel>> GetAllNodes(Guid roadmapId)
    {
        return _db.LoadData<NodeModel, dynamic>("dbo.spNode_GetAll", new { RoadmapId = roadmapId });
    }

    public async Task<NodeModel?> GetNodeById(Guid id)
    {
        var results = await _db.LoadData<NodeModel, dynamic>(
            "dbo.spNode_GetById",
            new { Id = id });

        return results.FirstOrDefault();
    }

    public Task AddNode(NodeModel node)
    {
        return _db.SaveData("dbo.spNode_Add", new
        {
            node.Id,
            node.Name,
            node.Description,
            node.CreatedDate,
            node.RoadmapId
        });
    }

    public Task UpdateNode(NodeModel node)
    {
        return _db.SaveData("dbo.spNode_Update", new
        {
            node.Id,
            node.Name,
            node.Description,
            node.UpdatedDate
        });
    }

    public Task DeleteNode(Guid id)
    {
        return _db.SaveData("dbo.spNode_Delete", new { Id = id });
    }
}
