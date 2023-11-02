using Site.Models;

namespace Site.Services.Registration;

public class RegistrationService : IRegistrationService
{
	private readonly HttpClient _client;
	private readonly IConfiguration _config;
	private readonly ILogger<RegistrationService> _logger;

	public RegistrationService(HttpClient client,
		IConfiguration config,
		ILogger<RegistrationService> logger)
	{
		_client = client;
		_config = config;
		_logger = logger;
	}

	public async Task<string?> RegisterUser(RegistrationModel registrationUser)
	{
		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("username", registrationUser.Username),
			new KeyValuePair<string, string>("email", registrationUser.Email),
			new KeyValuePair<string, string>("password", registrationUser.Password)
		});

		string registerEndpoint = _config["apiLocation"] + _config["registerEndpoint"];
		var authResult = await _client.PostAsync(registerEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger.LogError("Ocorreu um erro durante o registro de conta de usuario {authContent}", authContent);
			return null;
		}

		return await authResult.Content.ReadAsStringAsync();
	}
}
