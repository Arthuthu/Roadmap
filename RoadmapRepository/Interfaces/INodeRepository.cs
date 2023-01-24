using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface INodeRepository
    {
		Task<IEnumerable<NodeModel>> GetAllNodes();
		Task<NodeModel?> GetNodeById(Guid id);
		Task AddNode(NodeModel node);
		Task UpdateNode(NodeModel node);
		Task DeleteNode(Guid id);
    }
}