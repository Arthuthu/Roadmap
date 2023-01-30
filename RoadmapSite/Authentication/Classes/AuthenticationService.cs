using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapSite.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RoadmapSite.Authentication.Classes;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient client,
        AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", userForAuthentication.Username),
            new KeyValuePair<string, string>("password", userForAuthentication.Password)
        });

        var authResult = await _client.PostAsync("https://localhost:5001/token", data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            return null;
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync("authToken", result.Acess_Token);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Acess_Token);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
            result.Acess_Token);

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}
