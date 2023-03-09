using Blazored.LocalStorage;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Roadmap.Interfaces;

namespace RoadmapSite.Services.Roadmap.Classes;

public class RoadmapService : IRoadmapService
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly IConfiguration _config;
	private readonly ILogger<RoadmapService> _logger;

	public RoadmapService(HttpClient client,
		ILocalStorageService localStorage,
		IConfiguration config,
		ILogger<RoadmapService> logger)
	{
		_client = client;
		_localStorage = localStorage;
		_config = config;
		_logger = logger;
	}

	public async Task<string> CreateRoadmap(RoadmapClassModel roadmap)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("name", roadmap.Name),
			new KeyValuePair<string, string>("description", roadmap.Description),
			new KeyValuePair<string, string>("category", roadmap.Category),
			new KeyValuePair<string, string>("userId", roadmap.UserId.ToString())
		});

		string createRoadmapEndpoint = _config["apiLocation"] + _config["createroadmapEndpoint"];
		var authResult = await _client.PostAsync(createRoadmapEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante a criação do roadmap: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}

	public async Task<IList<RoadmapClassModel>?> GetAllRoadmaps()
	{
		string getAllRoadmapsEndpoint = _config["apiLocation"] + _config["getAllRoadmapsEndpoint"];
		var authResult = await _client.GetAsync(getAllRoadmapsEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento de roadmaps: {authContent}");
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
			_logger.LogInformation($"Ocorreu um erro durante o carregamento dos roadmaps do usuario: {authContent}");
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
			_logger.LogInformation($"Ocorreu um erro durante o carregamento do roadmap: {authContent}");
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<RoadmapClassModel>(authContent);

		return roadmapClassModel;
	}

    public async Task<string> UpdateRoadmap(RoadmapClassModel roadmap)
	{
        var data = new FormUrlEncodedContent(new[]
		{
            new KeyValuePair<string, string>("name", roadmap.Name),
            new KeyValuePair<string, string>("description", roadmap.Description),
            new KeyValuePair<string, string>("category", roadmap.Category),
            new KeyValuePair<string, string>("isapproved", roadmap.IsApproved.ToString()),
            new KeyValuePair<string, string>("ishidden", roadmap.IsHidden.ToString()),
            new KeyValuePair<string, string>("userId", roadmap.UserId.ToString())
        });

        string updateRoadmapEndpoint = _config["apiLocation"] + _config["updateRoadmapEndpoint"];
        var authResult = await _client.DeleteAsync(updateRoadmapEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogInformation($"Ocorreu um erro ao atualizar o roadmap: {authContent}");
            return null;
        }

        return await authResult.Content.ReadAsStringAsync();
    }


    public async Task<string> DeleteRoadmap(Guid id)
	{
		string deleteRoadmapEndpoint = _config["apiLocation"] + _config["deleteRoadmapEndpoint"] + $"/{id}";
		var authResult = await _client.DeleteAsync(deleteRoadmapEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento do roadmap: {authContent}");
			return authContent;
		}

		return authContent;
	}

}
