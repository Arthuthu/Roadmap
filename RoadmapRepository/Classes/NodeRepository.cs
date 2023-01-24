using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class NodeRepository : INodeRepository
{
	private readonly ISqlDataAccess _db;

	public NodeRepository(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<NodeModel>> GetAllNodes()
	{
		return _db.LoadData<NodeModel, dynamic>("dbo.spNode_GetAll", new { });
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
			node.RoadmapId
		});
	}

	public Task UpdateNode(NodeModel node)
	{
		return _db.SaveData("dbo.spNode_Update", node);
	}

	public Task DeleteNode(Guid id)
	{
		return _db.SaveData("dbo.spNode_Delete", new { Id = id });
	}
}
