using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Roadmap;

namespace RoadmapSite.Services.Node;

public class NodeService : INodeService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<RoadmapService> _logger;

    public NodeService(HttpClient client,
        IConfiguration config,
        ILogger<RoadmapService> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }
    public async Task<IList<NodeModel>?> GetAllNodes(Guid roadmapId)
    {
        string getAllNodesEndpoint = _config["apiLocation"] + _config["getAllNodesEndpoint"] + $"/{roadmapId}";
        var authResult = await _client.GetAsync(getAllNodesEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro durante o carregamento dos nodes: {authContent}",
                authContent);
            return null;
        }

        var nodeModel = JsonConvert.DeserializeObject<IList<NodeModel>>(authContent);

        return nodeModel;
    }
    public async Task<NodeModel?> GetNodeById(Guid? nodeId)
    {
        string getNodeByIdEndpoint = _config["apiLocation"] + _config["getNodeByIdEndpoint"] + $"/{nodeId}";
        var authResult = await _client.GetAsync(getNodeByIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro durante o carregamento do node: {authContent}",
                authContent);
            return null;
        }

        var nodeModel = JsonConvert.DeserializeObject<NodeModel>(authContent);

        return nodeModel;
    }
    public async Task<string?> CreateNode(NodeModel node)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("name", node.Name),
            new KeyValuePair<string, string>("description", node.Description!),
            new KeyValuePair<string, string>("roadmapid", node.RoadmapId.ToString())
        });

        string createNodeEndpoint = _config["apiLocation"] + _config["createNodeEndpoint"];
        var authResult = await _client.PostAsync(createNodeEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro durante a criação do node: {authContent}", authContent);
            return null;
        }

        return await authResult.Content.ReadAsStringAsync();
    }
    public async Task<string?> UpdateNode(NodeModel node)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", node.Id.ToString()),
            new KeyValuePair<string, string>("name", node.Name),
            new KeyValuePair<string, string>("description", node.Description!)
        });

        string updateNodeEndpoint = _config["apiLocation"] + _config["updateNodeEndpoint"];
        var authResult = await _client.PutAsync(updateNodeEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro ao atualizar o node: {authContent}", authContent);
            return null;
        }

        return await authResult.Content.ReadAsStringAsync();
    }
    public async Task<string?> DeleteNode(Guid nodeId)
    {
        string deleteNodeEndpoint = _config["apiLocation"] + _config["deleteNodeEndpoint"] + $"/{nodeId}";
        var authResult = await _client.DeleteAsync(deleteNodeEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para deletar o node: {authContent}", authContent);
            return null;
        }

        return authContent;
    }
}
