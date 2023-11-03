using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapBlazor.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RoadmapBlazor.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly string authTokenStorageKey;

    public AuthenticationService(HttpClient client,
        AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage,
        IConfiguration config,
        ILogger<AuthenticationService> logger)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
        _config = config;
        _logger = logger;
        authTokenStorageKey = _config["authTokenStorageKey"]!;
    }

    public async Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", userForAuthentication.Username),
            new KeyValuePair<string, string>("password", userForAuthentication.Password)
        });

        string loginEndpoint = _config["apiLocation"] + _config["loginEndpoint"];
        var authResult = await _client.PostAsync(loginEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o login {authContent}",
                authContent);
            return null;
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
            authContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync(authTokenStorageKey, result!.Access_Token);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Access_Token);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
            result.Access_Token);

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(authTokenStorageKey);
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}
