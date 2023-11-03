using Newtonsoft.Json;
using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.ComentarioVotes;

public class ComentarioVotesService : IComentarioVotesService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<ComentarioVotesService> _logger;

    public ComentarioVotesService(HttpClient client,
        IConfiguration config,
        ILogger<ComentarioVotesService> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }
    public async Task<IList<ComentarioVotesModel>?> GetAllComentarioVotes(Guid? userId, Guid comentarioId)
    {
        string getAllComentariosVotesEndpoint = _config["apiLocation"] + _config["getAllComentarioVotesEndpoint"] + $"/{userId}" + $"/{comentarioId}";
        var authResult = await _client.GetAsync(getAllComentariosVotesEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro durante o carregamento dos votos: {authContent}",
                authContent);
            return null;
        }

        var comentarioVotesModel = JsonConvert.DeserializeObject<IList<ComentarioVotesModel>>(authContent);

        return comentarioVotesModel;
    }

    public async Task<string?> AddComentarioVote(Guid? userId, Guid comentarioId)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("userId", userId.ToString()!),
            new KeyValuePair<string, string>("comentarioId", comentarioId.ToString())
        });

        string addComentarioVoteEndpoint = _config["apiLocation"] + _config["addComentarioVoteEndpoint"] + $"/{userId}" + $"/{comentarioId}";
        var authResult = await _client.PostAsync(addComentarioVoteEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para adicionar o voto: {authContent}",
                authContent);
            return null;
        }

        return await authResult.Content.ReadAsStringAsync();
    }
    public async Task<string?> RemoveComentarioVote(Guid comentarioVoteId)
    {
        string removeComentarioVoteEndpoint = _config["apiLocation"] + _config["removeComentarioVoteEndpoint"] + $"/{comentarioVoteId}";
        var authResult = await _client.DeleteAsync(removeComentarioVoteEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para remover o voto: {authContent}",
                authContent);
            return null;
        }

        return authContent;
    }
}
