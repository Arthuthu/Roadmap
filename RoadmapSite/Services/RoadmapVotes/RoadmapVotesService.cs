using Newtonsoft.Json;
using Site.Models;

namespace Site.Services.RoadmapVotes;

public class RoadmapVotesService : IRoadmapVotesService
{
	private readonly HttpClient _client;
	private readonly IConfiguration _config;
	private readonly ILogger<RoadmapVotesService> _logger;

	public RoadmapVotesService(HttpClient client,
		IConfiguration config,
		ILogger<RoadmapVotesService> logger)
	{
		_client = client;
		_config = config;
		_logger = logger;
	}
	public async Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotes(Guid? userId, Guid roadmapId)
	{
		string getAllRoadmapsVotesEndpoint = _config["apiLocation"] + _config["getAllRoadmapVotesEndpoint"] + $"/{userId}" + $"/{roadmapId}";
		var authResult = await _client.GetAsync(getAllRoadmapsVotesEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro durante o carregamento dos votos: {authContent}", authContent);
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<IList<RoadmapVotesModel>>(authContent);

		return roadmapVotesModel;
	}
	public async Task<IList<RoadmapVotesModel>?> GetAllRoadmapVotesByUserId(Guid? userId)
	{
		string getallroadmapvotesbyuseridEndpoint = _config["apiLocation"] + _config["getallroadmapvotesbyuseridEndpoint"] + $"/{userId}";
		var authResult = await _client.GetAsync(getallroadmapvotesbyuseridEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro durante o carregamento dos votos pelo usuario: {authContent}",
				authContent);
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<IList<RoadmapVotesModel>>(authContent);

		return roadmapVotesModel;
	}
	public async Task<string?> AddRoadmapVote(Guid? userId, Guid roadmapId)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("userId", userId.ToString()!),
			new KeyValuePair<string, string>("roadmapId", roadmapId.ToString())
		});

		string addRoadmapVoteEndpoint = _config["apiLocation"] + _config["addRoadmapVoteEndpoint"] + $"/{userId}" + $"/{roadmapId}";
		var authResult = await _client.PostAsync(addRoadmapVoteEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro para adicionar o voto: {authContent}", authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> RemoveRoadmapVote(Guid roadmapVoteId)
	{
		string removeRoadmapVoteEndpoint = _config["apiLocation"] + _config["removeRoadmapVoteEndpoint"] + $"/{roadmapVoteId}";
		var authResult = await _client.DeleteAsync(removeRoadmapVoteEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro para remover o voto: {authContent}", authContent);
			return null;
		}

		return authContent;
	}
}
