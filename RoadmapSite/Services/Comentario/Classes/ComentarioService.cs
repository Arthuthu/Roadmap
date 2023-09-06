using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Comentario.Interfaces;

namespace RoadmapSite.Services.Comentario.Classes;

public class ComentarioService : IComentarioService
{
	private readonly HttpClient _client;
	private readonly IConfiguration _config;
	private readonly ILogger<ComentarioService> _logger;

	public ComentarioService(HttpClient client,
		IConfiguration config,
		ILogger<ComentarioService> logger)
	{
		_client = client;
		_config = config;
		_logger = logger;
	}

	public async Task<IList<ComentarioModel>?> GetAllComentariosByRoadmapId(Guid roadmapid)
	{
		string getAllComentariosByRoadmapIdEndpoint = _config["apiLocation"] + _config["getAllComentariosByRoadmapIdEndpoint"] + $"/{roadmapid}";
		var authResult = await _client.GetAsync(getAllComentariosByRoadmapIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento dos comentarios: {authContent}",
				authContent);
			return null;
		}

		var comentarioModel = JsonConvert.DeserializeObject<IList<ComentarioModel>>(authContent);

		return comentarioModel;
	}
	public async Task<ComentarioModel?> GetComentarioById(Guid? comentarioId)
	{
		string getComentarioByIdEndpoint = _config["apiLocation"] + _config["getComentarioByIdEndpoint"] + $"/{comentarioId}";
		var authResult = await _client.GetAsync(getComentarioByIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do comentario: {authContent}",
				authContent);
			return null;
		}

		var comentarioModel = JsonConvert.DeserializeObject<ComentarioModel>(authContent);

		return comentarioModel;
	}
	public async Task<string?> CreateComentario(ComentarioModel comentario)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("description", comentario.Description),
			new KeyValuePair<string, string>("authorname", comentario.AuthorName),
			new KeyValuePair<string, string>("userId", comentario.UserId.ToString()!),
			new KeyValuePair<string, string>("roadmapId", comentario.RoadmapId.ToString()),
		});

		string createComentarioEndpoint = _config["apiLocation"] + _config["createComentarioEndpoint"];
		var authResult = await _client.PostAsync(createComentarioEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante a criação do comentario: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> UpdateComentario(ComentarioModel comentario)
	{
		var data = new FormUrlEncodedContent(new[]
		{
				new KeyValuePair<string, string>("id", comentario.Id.ToString()),
				new KeyValuePair<string, string>("comentario", comentario.Description),
				new KeyValuePair<string, string>("userid", comentario.UserId.ToString()!)
			});

		string updateComentarioEndpoint = _config["apiLocation"] + _config["updateComentarioEndpoint"];
		var authResult = await _client.PutAsync(updateComentarioEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar o comentario: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> DeleteComentario(Guid id)
	{
		string deleteComentarioEndpoint = _config["apiLocation"] + _config["deleteComentarioEndpoint"] + $"/{id}";
		var authResult = await _client.DeleteAsync(deleteComentarioEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro para deletar o comentario: {authContent}",
				authContent);
			return null;
		}

		return authContent;
	}
	public async Task<string?> DeleteAllUserComentarios(Guid userId)
	{
		string deleteAllUserComentariosEndpoint = _config["apiLocation"] + _config["deleteAllUserComentariosEndpoint"] + $"/{userId}";
		var authResult = await _client.DeleteAsync(deleteAllUserComentariosEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro para deletar os comentarios: {authContent}",
				authContent);
			return null;
		}

		return authContent;
	}
}
