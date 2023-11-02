using Newtonsoft.Json;
using Site.Models;

namespace Site.Services.Roadmap;

public class RoadmapService : IRoadmapService
{
	private readonly HttpClient _client;
	private readonly IConfiguration _config;
	private readonly ILogger<RoadmapService> _logger;

	public RoadmapService(HttpClient client,
		IConfiguration config,
		ILogger<RoadmapService> logger)
	{
		_client = client;
		_config = config;
		_logger = logger;
	}
	public async Task<IList<RoadmapClassModel>?> GetAllRoadmaps()
	{
		string getAllRoadmapsEndpoint = _config["apiLocation"] + _config["getAllRoadmapsEndpoint"];
		var authResult = await _client.GetAsync(getAllRoadmapsEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento de roadmaps: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<RoadmapClassModel>>(authContent);

		return roadmapClassModel;
	}
	public async Task<IList<RoadmapClassModel>?> GetAllApprovedRoadmaps()
	{
		string getAllApprovedRoadmapsEndpoint = _config["apiLocation"] + _config["getAllApprovedRoadmapsEndpoint"];
		var authResult = await _client.GetAsync(getAllApprovedRoadmapsEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento de roadmaps aprovados: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<RoadmapClassModel>>(authContent);

		return roadmapClassModel;
	}
	public async Task<IList<RoadmapClassModel>?> GetAllNotApprovedRoadmaps()
	{
		string getAllNotApprovedRoadmapsEndpoint = _config["apiLocation"] + _config["getAllNotApprovedRoadmapsEndpoint"];
		var authResult = await _client.GetAsync(getAllNotApprovedRoadmapsEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento de roadmaps não aprovados: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<RoadmapClassModel>>(authContent);

		return roadmapClassModel;
	}
	public async Task<IList<RoadmapClassModel>?> GetAllApprovedRoadmapsByCategory()
	{
		string getAllApprovedRoadmapsByCategory = _config["apiLocation"] + _config["getAllApprovedRoadmapsByCategory"];
		var authResult = await _client.GetAsync(getAllApprovedRoadmapsByCategory);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento dos roadmaps por categoria: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<RoadmapClassModel>>(authContent);

		return roadmapClassModel;
	}
	public async Task<IList<RoadmapClassModel>?> GetRoadmapByUserId(Guid userId)
	{
		string getRoadmapByUserIdEndpoint = _config["apiLocation"] + _config["getRoadmapByUserIdEndpoint"] + $"/{userId}";
		var authResult = await _client.GetAsync(getRoadmapByUserIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento dos roadmaps do usuario: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<RoadmapClassModel>>(authContent);

		return roadmapClassModel;
	}
	public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
	{
		string getRoadmapByIdEndpoint = _config["apiLocation"] + _config["getRoadmapByIdEndpoint"] + $"/{id}";
		var authResult = await _client.GetAsync(getRoadmapByIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do roadmap: {authContent}",
				authContent);
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<RoadmapClassModel>(authContent);

		return roadmapClassModel;
	}
	public async Task<string?> CreateRoadmap(RoadmapClassModel roadmap)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("name", roadmap.Name!),
			new KeyValuePair<string, string>("description", roadmap.Description!),
			new KeyValuePair<string, string>("category", roadmap.Category!),
			new KeyValuePair<string, string>("authorname", roadmap.AuthorName),
			new KeyValuePair<string, string>("userId", roadmap.UserId.ToString())
		});

		string createRoadmapEndpoint = _config["apiLocation"] + _config["createroadmapEndpoint"];
		var authResult = await _client.PostAsync(createRoadmapEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante a criação do roadmap: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> UpdateRoadmap(RoadmapClassModel roadmap)
	{
		var data = new FormUrlEncodedContent(new[]
		{
				new KeyValuePair<string, string>("id", roadmap.Id.ToString()),
				new KeyValuePair<string, string>("name", roadmap.Name),
				new KeyValuePair<string, string>("description", roadmap.Description!),
				new KeyValuePair<string, string>("category", roadmap.Category!),
				new KeyValuePair<string, string>("isapproved", roadmap.IsApproved.ToString()),
				new KeyValuePair<string, string>("userId", roadmap.UserId.ToString())
			});

		string updateRoadmapEndpoint = _config["apiLocation"] + _config["updateRoadmapEndpoint"];
		var authResult = await _client.PutAsync(updateRoadmapEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar o roadmap: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> DeleteRoadmap(Guid id)
	{
		string deleteRoadmapEndpoint = _config["apiLocation"] + _config["deleteRoadmapEndpoint"] + $"/{id}";
		var authResult = await _client.DeleteAsync(deleteRoadmapEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro para deletar o roadmap: {authContent}",
				authContent);
			return null;
		}

		return authContent;
	}
	public async Task<string?> DeleteAllUserRoadmaps(Guid userId)
	{
		string deleteAllUserRoadmapsEndpoint = _config["apiLocation"] + _config["deleteAllUserRoadmapsEndpoint"] + $"/{userId}";
		var authResult = await _client.DeleteAsync(deleteAllUserRoadmapsEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro para deletar os roadmaps: {authContent}",
				authContent);
			return null;
		}

		return authContent;
	}
}
