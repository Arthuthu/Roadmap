using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapSite.Authentication.Interfaces;
using RoadmapSite.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoadmapSite.Authentication.Classes;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private string authTokenStorageKey;

    public AuthenticationService(HttpClient client,
        AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage,
        IConfiguration config)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
        _config = config;
        authTokenStorageKey = _config["authTokenStorageKey"];
    }

    public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", userForAuthentication.Username),
            new KeyValuePair<string, string>("password", userForAuthentication.Password)
        });

        string loginEndpoint = _config["apiLocation"] + _config["registerEndpoint"];
        var authResult = await _client.PostAsync(loginEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            return null;
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
            authContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync(authTokenStorageKey, result.Access_Token);

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
