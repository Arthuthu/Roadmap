using RoadmapSite.Models;
using RoadmapSite.Registration.Interfaces;
using System.Text.Json;

namespace RoadmapSite.Registration.Classes;

public class RegistrationService : IRegistrationService
{
	private readonly HttpClient _client;
	private readonly IConfiguration _config;

	public RegistrationService(HttpClient client, IConfiguration config)
	{
		_client = client;
		_config = config;
	}

	public async Task<RegistrationModel> RegisterUser(RegistrationModel registrationUser)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("username", registrationUser.Username),
			new KeyValuePair<string, string>("password", registrationUser.Password)
		});

		string registerEndpoint = _config["apiLocation"] + _config["registerEndpoint"];
		var authResult = await _client.PostAsync(registerEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			return null;
		}

		return registrationUser;
	}
}
