using Blazored.LocalStorage;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Node.Interfaces;
using RoadmapSite.Services.Roadmap.Classes;

namespace RoadmapSite.Services.Node.Classes;

public class NodeService : INodeService
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly IConfiguration _config;
	private readonly ILogger<RoadmapService> _logger;

	public NodeService(HttpClient client,
		ILocalStorageService localStorage,
		IConfiguration config,
		ILogger<RoadmapService> logger)
	{
		_client = client;
		_localStorage = localStorage;
		_config = config;
		_logger = logger;
	}

	public async Task<string> CreateNode(NodeModel node)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("name", node.Name),
			new KeyValuePair<string, string>("description", node.Description),
			new KeyValuePair<string, string>("roadmapid", node.RoadmapId.ToString())
		});

		string createNodeEndpoint = _config["apiLocation"] + _config["createNodeEndpoint"];
		var authResult = await _client.PostAsync(createNodeEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante a criação do node: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<IList<NodeModel>?> GetAllNodes(Guid roadmapId)
	{
		string getAllNodesEndpoint = _config["apiLocation"] + _config["getAllNodesEndpoint"] + $"/{roadmapId}";
		var authResult = await _client.GetAsync(getAllNodesEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento dos nodes: {authContent}");
			return null;
		}

		var nodeModel = JsonConvert.DeserializeObject<IList<NodeModel>>(authContent);

		return nodeModel;
	}

	public async Task<string> DeleteNode(Guid nodeId)
	{
		string deleteNodeEndpoint = _config["apiLocation"] + _config["deleteNodeEndpoint"] + $"/{nodeId}";
		var authResult = await _client.DeleteAsync(deleteNodeEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para deletar o node: {authContent}");
			return authContent;
		}

		return authContent;
	}
}
