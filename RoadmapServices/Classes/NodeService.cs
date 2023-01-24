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

	public Task<IEnumerable<NodeModel>> GetAllNodes()
	{
		return _nodeData.GetAllNodes();
	}

	public async Task<NodeModel?> GetNodeById(Guid id)
	{
		return await _nodeData.GetNodeById(id);
	}

	public async Task AddNode(NodeModel node)
	{
		await _nodeData.AddNode(node);
	}

	public Task UpdateNode(NodeModel node)
	{
		return _nodeData.UpdateNode(node);
	}

	public Task DeleteNode(Guid id)
	{
		return _nodeData.DeleteNode(id);
	}
}
