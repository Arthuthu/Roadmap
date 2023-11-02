using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Site.Models;
using System.Security.Claims;

namespace Site.Services.User;

public class UserService : IUserService
{
	private readonly HttpClient _client;
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	private readonly NavigationManager _navigationManager;
	private readonly IConfiguration _config;
	private readonly ILogger<UserService> _logger;

	public UserService(HttpClient client,
	AuthenticationStateProvider authenticationStateProvider,
	NavigationManager navigationManager,
	IConfiguration config,
	ILogger<UserService> logger)
	{
		_client = client;
		_authenticationStateProvider = authenticationStateProvider;
		_navigationManager = navigationManager;
		_config = config;
		_logger = logger;
	}
	public async Task<IList<UserModel>?> GetAllUsers()
	{
		string getAllUsersEndpoint = _config["apiLocation"] + _config["getAllUsersEndpoint"];
		var authResult = await _client.GetAsync(getAllUsersEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento de usuarios: {authContent}",
				authContent);
			return null;
		}

		var userModel = JsonConvert.DeserializeObject<IList<UserModel>>(authContent);

		return userModel;
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
	public async Task<UserModel?> GetUserById(Guid? userId)
	{
		string getUserByIdEndpoint = _config["apiLocation"] + _config["getUserByIdEndpoint"] + $"/{userId}";
		var authResult = await _client.GetAsync(getUserByIdEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do usuario por id: {authContent}",
				authContent);
			return null;
		}

		var userModel = JsonConvert.DeserializeObject<UserModel>(authContent);

		return userModel;
	}
	public async Task<UserModel?> GetUserByName(string? username)
	{
		string getUserByNameEndpoint = _config["apiLocation"] + _config["getUserByNameEndpoint"] + $"/{username}";
		var authResult = await _client.GetAsync(getUserByNameEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do usuario por nome: {authContent}",
				authContent);
			return null;
		}

		var userModel = JsonConvert.DeserializeObject<UserModel>(authContent);

		return userModel;
	}

	public async Task<UserModel?> GetUserByConfirmationCode(Guid? confirmationCode)
	{
		string getUserByConfirmationCodeEndpoint = _config["apiLocation"] + _config["getUserByConfirmationCodeEndpoint"] + $"/{confirmationCode}";
		var authResult = await _client.GetAsync(getUserByConfirmationCodeEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do usuario com o codigo de confirmação: {authContent}",
				authContent);
			return null;
		}

		var userModel = JsonConvert.DeserializeObject<UserModel>(authContent);

		return userModel;
	}
	public async Task<UserModel?> GetUserByRestorationCode(Guid? restorationCode)
	{
		string getUserByRestorationCodeEndpoint = _config["apiLocation"] + _config["getUserByRestorationCodeEndpoint"] + $"/{restorationCode}";
		var authResult = await _client.GetAsync(getUserByRestorationCodeEndpoint);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro durante o carregamento do usuario com o codigo de restauracao: {authContent}",
				authContent);
			return null;
		}

		var userModel = JsonConvert.DeserializeObject<UserModel>(authContent);

		return userModel;
	}
	public async Task<string?> UpdateUser(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
{
			new KeyValuePair<string, string>("id", user.Id.ToString()),
			new KeyValuePair<string, string>("username", user.Username!),
			new KeyValuePair<string, string>("password", user.Password!),
			new KeyValuePair<string, string>("bio", user.Bio!),
			new KeyValuePair<string, string>("isbanned", user.IsBanned.ToString())
		});

		string updateUserEndpoint = _config["apiLocation"] + _config["updateUserEndpoint"];
		var authResult = await _client.PutAsync(updateUserEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar o perfil: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> UpdateUserEmailConfirmation(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("id", user.Id.ToString()),
			new KeyValuePair<string, string>("confirmationcode", user.ConfirmationCode.ToString()!),
			new KeyValuePair<string, string>("confirmationcodeexpirationdate", user.ConfirmationCodeExpirationDate.ToString()!),
			new KeyValuePair<string, string>("isconfirmed", user.IsConfirmed.ToString())
		});

		string updateUserEmailConfirmationEndpoint = _config["apiLocation"] + _config["updateUserEmailConfirmationEndpoint"];
		var authResult = await _client.PutAsync(updateUserEmailConfirmationEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar a confirmação de email: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> UpdateUserPassword(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("id", user.Id.ToString()),
			new KeyValuePair<string, string>("password", user.Password!)
		});

		string updateUserPasswordEndpoint = _config["apiLocation"] + _config["updateUserPasswordEndpoint"];
		var authResult = await _client.PutAsync(updateUserPasswordEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar a senha: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> SendConfirmationEmail(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("email", user.Email),
		});

		string sendConfirmationEmailEndpoint = _config["apiLocation"] + _config["sendConfirmationEmailEndpoint"];
		var authResult = await _client.PutAsync(sendConfirmationEmailEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogInformation("Ocorreu um erro ao enviar email de confirmação: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
	public async Task<string?> SendRestorationEmail(UserModel user)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("email", user.Email),
		});

		string sendRestorationEmailEndpoint = _config["apiLocation"] + _config["sendRestorationEmailEndpoint"];
		var authResult = await _client.PutAsync(sendRestorationEmailEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogInformation("Ocorreu um erro ao resgatar a conta: {authContent}",
				authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
}