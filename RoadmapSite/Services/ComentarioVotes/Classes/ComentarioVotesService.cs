using Blazored.LocalStorage;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.ComentarioVotes.Interfaces;

namespace ComentarioSite.Services.ComentarioVotes.Classes;

public class ComentarioVotesService : IComentarioVotesService
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly IConfiguration _config;
	private readonly ILogger<ComentarioVotesService> _logger;

	public ComentarioVotesService(HttpClient client,
		ILocalStorageService localStorage,
		IConfiguration config,
		ILogger<ComentarioVotesService> logger)
	{
		_client = client;
		_localStorage = localStorage;
		_config = config;
		_logger = logger;
	}

	public async Task<string?> AddComentarioVote(Guid? userId, Guid comentarioId)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("userId", userId.ToString()),
			new KeyValuePair<string, string>("comentarioId", comentarioId.ToString())
		});

		string addComentarioVoteEndpoint = _config["apiLocation"] + _config["addComentarioVoteEndpoint"] + $"/{userId}" + $"/{comentarioId}";
		var authResult = await _client.PostAsync(addComentarioVoteEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para adicionar o voto: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<IList<ComentarioVotesModel>?> GetAllComentarioVotes(Guid? userId, Guid comentarioId)
	{
		string getAllComentariosVotesEndpoint = _config["apiLocation"] + _config["getAllComentarioVotesEndpoint"] + $"/{userId}" + $"/{comentarioId}";
		var authResult = await _client.GetAsync(getAllComentariosVotesEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento dos votos: {authContent}");
			return null;
		}

		var comentarioVotesModel = JsonConvert.DeserializeObject<IList<ComentarioVotesModel>>(authContent);

		return comentarioVotesModel;
	}

	public async Task<string?> RemoveComentarioVote(Guid comentarioVoteId)
	{
		string removeComentarioVoteEndpoint = _config["apiLocation"] + _config["removeComentarioVoteEndpoint"] + $"/{comentarioVoteId}";
		var authResult = await _client.DeleteAsync(removeComentarioVoteEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para remover o voto: {authContent}");
			return null;
		}

		return authContent;
	}
}
