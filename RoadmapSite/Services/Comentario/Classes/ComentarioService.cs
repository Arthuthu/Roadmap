using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Comentario.Interfaces;
using RoadmapSite.Services.Registration.Classes;

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

	public async Task<string> CreateComentario(ComentarioModel comentario)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("description", comentario.Description),
			new KeyValuePair<string, string>("userId", comentario.UserId.ToString()),
			new KeyValuePair<string, string>("roadmapId", comentario.RoadmapId.ToString()),
		});

		string createComentarioEndpoint = _config["apiLocation"] + _config["createComentarioEndpoint"];
		var authResult = await _client.PostAsync(createComentarioEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante a criação do comentario: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<IList<ComentarioModel>?> GetAllComentarios()
	{
		string getAllComentariosEndpoint = _config["apiLocation"] + _config["getAllComentariosEndpoint"];
		var authResult = await _client.GetAsync(getAllComentariosEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento dos comentarios: {authContent}");
			return null;
		}

		var comentarioModel = JsonConvert.DeserializeObject<IList<ComentarioModel>>(authContent);

		return comentarioModel;
	}

	public async Task<string> UpdateComentario(ComentarioModel comentario)
	{
		var data = new FormUrlEncodedContent(new[]
		{
				new KeyValuePair<string, string>("id", comentario.Id.ToString()),
				new KeyValuePair<string, string>("comentario", comentario.Description),
				new KeyValuePair<string, string>("userid", comentario.UserId.ToString())
			});

		string updateComentarioEndpoint = _config["apiLocation"] + _config["updateComentarioEndpoint"];
		var authResult = await _client.PutAsync(updateComentarioEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro ao atualizar o comentario: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<string> DeleteComentario(Guid id)
	{
		string deleteComentarioEndpoint = _config["apiLocation"] + _config["deleteComentarioEndpoint"] + $"/{id}";
		var authResult = await _client.DeleteAsync(deleteComentarioEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro para deletar o comentario: {authContent}");
			return authContent;
		}

		return authContent;
	}
}
