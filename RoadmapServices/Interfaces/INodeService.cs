using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface INodeService
    {
		Task<IEnumerable<NodeModel>> GetAllNodes(Guid roadmapId);
		Task<NodeModel?> GetNodeById(Guid id);
		Task AddNode(NodeModel node);
		Task UpdateNode(NodeModel node);
		Task DeleteNode(Guid id);
    }
}