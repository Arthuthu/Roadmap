﻿using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.Node;

public interface INodeService
{
    Task<IList<NodeModel>?> GetAllNodes(Guid id);
    Task<NodeModel?> GetNodeById(Guid? nodeId);
    Task<string?> CreateNode(NodeModel node);
    Task<string?> UpdateNode(NodeModel node);
    Task<string?> DeleteNode(Guid nodeId);
}