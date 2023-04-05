using RoadmapSite.Models;

namespace RoadmapSite.Services.Node.Interfaces
{
    public interface INodeService
    {
        Task<string> CreateNode(NodeModel node);
        Task<IList<NodeModel>?> GetAllNodes(Guid id);
        Task<NodeModel> GetNodeById(Guid? nodeId);

		Task<string> DeleteNode(Guid nodeId);
	}
}