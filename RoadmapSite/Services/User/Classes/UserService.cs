using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Roadmap.Classes;
using RoadmapSite.Services.User.Interfaces;
using System.Security.Claims;

namespace RoadmapSite.Services.User.Classes;

public class UserService : IUserService
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	private readonly NavigationManager _navigationManager;
	private readonly IConfiguration _config;
	private readonly ILogger<UserService> _logger;

	public UserService(HttpClient client,
	AuthenticationStateProvider authenticationStateProvider,
	NavigationManager navigationManager,
	ILocalStorageService localStorage,
	IConfiguration config,
	ILogger<UserService> logger)
	{
		_client = client;
		_localStorage = localStorage;
		_authenticationStateProvider = authenticationStateProvider;
		_navigationManager = navigationManager;
		_config = config;
		_logger = logger;
	}

	public async Task<Guid> GetLoggedInUserId()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		var user = authenticationState.User;
		var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (userId is null)
		{
			return Guid.Empty;
		}

		var expirationClaim = user.FindFirst(ClaimTypes.Expiration);

		if (expirationClaim is not null)
		{
			var expirationDate = DateTime.Parse(expirationClaim.Value);
			if (expirationDate < DateTime.UtcNow)
			{
				_navigationManager.NavigateTo("/expiration");
				return Guid.Empty;
			}
		}

		return new Guid(userId);
	}
	public async Task<IList<UserModel>> GetAllUsers()
	{
		string getAllUsersEndpoint = _config["apiLocation"] + _config["getAllUsersEndpoint"];
		var authResult = await _client.GetAsync(getAllUsersEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro durante o carregamento de usuarios: {authContent}");
			return null;
		}

		var roadmapClassModel = JsonConvert.DeserializeObject<IList<UserModel>>(authContent);

		return roadmapClassModel;
	}

	public async Task<UserModel> GetUserById(Guid? userId)
	{
        string getUserByIdEndpoint = _config["apiLocation"] + _config["getUserByIdEndpoint"] + $"/{userId}";
        var authResult = await _client.GetAsync(getUserByIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogInformation($"Ocorreu um erro durante o carregamento do usuario: {authContent}");
            return null;
        }

        var roadmapClassModel = JsonConvert.DeserializeObject<UserModel>(authContent);

        return roadmapClassModel;
    }

	public async Task<string> UpdateUser(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
{
			new KeyValuePair<string, string>("id", user.Id.ToString()),
			new KeyValuePair<string, string>("username", user.Username!),
			new KeyValuePair<string, string>("password", user.Password),
			new KeyValuePair<string, string>("bio", user.Bio),
			new KeyValuePair<string, string>("isbanned", user.IsBanned)
        });

		string updateUserEndpoint = _config["apiLocation"] + _config["updateUserEndpoint"];
		var authResult = await _client.PutAsync(updateUserEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogInformation($"Ocorreu um erro ao atualizar o perfil: {authContent}");
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
}
