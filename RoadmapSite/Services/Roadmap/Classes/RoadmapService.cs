﻿using Blazored.LocalStorage;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Authentication.Classes;
using RoadmapSite.Services.Roadmap.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

	public async Task<IList<RoadmapClassModel>> GetAllRoadmaps()
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
}