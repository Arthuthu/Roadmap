using Blazored.LocalStorage;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Roadmap.Classes;
using RoadmapSite.Services.RoadmapVotes.Interfaces;

namespace RoadmapSite.Services.RoadmapVotes.Classes;

public class RoadmapVotesService : IRoadmapVotesService
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly IConfiguration _config;
	private readonly ILogger<RoadmapService> _logger;

	public RoadmapVotesService(HttpClient client,
		ILocalStorageService localStorage,
		IConfiguration config,
		ILogger<RoadmapService> logger)
	{
		_client = client;
		_localStorage = localStorage;
		_config = config;
		_logger = logger;
	}

	public async Task<string?> AddRoadmapVote(RoadmapVotesModel roadmapVote)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("userId", roadmapVote.UserId.ToString()!),
			new KeyValuePair<string, string>("roadmapId", roadmapVote.RoadmapId.ToString())
		});

		string addRoadmapVoteEndpoint = _config["apiLocation"] + _config["addRoadmapVoteEndpoint"];
		var authResult = await _client.PostAsync(addRoadmapVoteEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para adicionar o voto: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<IList<RoadmapVotesModel>?> GetAllRoadmaps()
	{
		string getAllRoadmapsVotesEndpoint = _config["apiLocation"] + _config["getAllRoadmapVotesEndpoint"];
		var authResult = await _client.GetAsync(getAllRoadmapsVotesEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento dos votos: {authContent}");
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<IList<RoadmapVotesModel>>(authContent);

		return roadmapVotesModel;
	}

	public async Task<IList<RoadmapVotesModel>?> GetAllRoadmapsUserVoted(Guid? userId)
	{
		string getAllRoadmapsUserVotedEndpoint = _config["apiLocation"] + _config["getAllRoadmapsUserVotedEndpoint"] + $"/{userId}";
		var authResult = await _client.GetAsync(getAllRoadmapsUserVotedEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para pegar a lista de roadmaps votados: {authContent}");
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<IList<RoadmapVotesModel>>(authContent);

		return roadmapVotesModel;
	}


	public async Task<RoadmapVotesModel?> GetRoadmapVoteIdByUserAndRoadmapId(Guid? userId, Guid roadmapId)
	{
		string getRoadmapVotedIdByUserAndRoadmapIdEndpoint = _config["apiLocation"] + _config["getRoadmapVotedIdByUserAndRoadmapIdEndpoint"] + $"/{userId}" + $"/{roadmapId}";
		var authResult = await _client.GetAsync(getRoadmapVotedIdByUserAndRoadmapIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para pegar o Id do roadmap em especifico: {authContent}");
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<RoadmapVotesModel>(authContent);

		return roadmapVotesModel;
	}

	public async Task<IList<RoadmapVotesModel>?> GetRoadmapVotesByRoadmapId(Guid roadmapId)
	{
		string getRoadmapVotesByRoadmapIdEndpoint = _config["apiLocation"] + _config["getRoadmapVotesByRoadmapIdEndpoint"] + $"/{roadmapId}";
		var authResult = await _client.GetAsync(getRoadmapVotesByRoadmapIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro ao carregar os votos do roadmap: {authContent}");
			return null;
		}

		var roadmapVotesModel = JsonConvert.DeserializeObject<IList<RoadmapVotesModel>>(authContent);

		return roadmapVotesModel;
	}

	public async Task<string?> RemoveRoadmapVote(Guid roadmapVoteId)
	{
		string removeRoadmapVoteEndpoint = _config["apiLocation"] + _config["removeRoadmapVoteEndpoint"] + $"/{roadmapVoteId}";
		var authResult = await _client.DeleteAsync(removeRoadmapVoteEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para remover o voto: {authContent}");
			return null;
		}

		return authContent;
	}
}
