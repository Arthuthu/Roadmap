using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace NodeServices.Classes;

public class NodeService : INodeService
{
	private readonly INodeRepository _nodeData;

	public NodeService(INodeRepository nodeData)
	{
		_nodeData = nodeData;
	}

	public Task<IEnumerable<NodeModel>> GetAllNodes(Guid roadmapId)
	{
		return _nodeData.GetAllNodes(roadmapId);
	}

	public async Task<NodeModel?> GetNodeById(Guid id)
	{
		return await _nodeData.GetNodeById(id);
	}

	public async Task AddNode(NodeModel node)
	{
		node.Id = Guid.NewGuid();
		node.CreatedDate = DateTime.UtcNow.AddHours(-3);

		try
		{
			await _nodeData.AddNode(node);
		}
		catch (Exception ex)
		{
			throw new Exception($"Ocorreu um erro ao adicionado o node {ex.Message}");
		}
	}

	public Task UpdateNode(NodeModel node)
	{
		node.UpdatedDate = DateTime.UtcNow.AddHours(-3);
		return _nodeData.UpdateNode(node);
	}

	public Task DeleteNode(Guid id)
	{
		return _nodeData.DeleteNode(id);
	}
}
