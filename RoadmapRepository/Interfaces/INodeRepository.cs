using Roadmap.Domain.Models;

namespace Roadmap.Domain.Interfaces
{
    public interface INodeRepository
    {
        Task<IEnumerable<NodeModel>> GetAllNodes(Guid roadmapId);
        Task<NodeModel?> GetNodeById(Guid id);
        Task AddNode(NodeModel node);
        Task UpdateNode(NodeModel node);
        Task DeleteNode(Guid id);
    }
}