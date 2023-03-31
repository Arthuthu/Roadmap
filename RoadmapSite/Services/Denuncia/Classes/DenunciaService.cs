using Newtonsoft.Json;
using RoadmapSite.Models;
using RoadmapSite.Services.Denuncia.Interfaces;

namespace RoadmapSite.Services.Denuncia.Classes;

public class DenunciaService : IDenunciaService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<DenunciaService> _logger;

    public DenunciaService(HttpClient client,
        IConfiguration config,
        ILogger<DenunciaService> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }

    public async Task<string> CreateDenuncia(DenunciaModel denuncia)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("description", denuncia.Description),
            new KeyValuePair<string, string>("type", denuncia.Type),
            new KeyValuePair<string, string>("userId", denuncia.UserId.ToString()),
            new KeyValuePair<string, string>("roadmapId", denuncia.RoadmapId.ToString()),
            new KeyValuePair<string, string>("commentId", denuncia.CommentId.ToString()),
        });

        string createDenunciaEndpoint = _config["apiLocation"] + _config["createDenunciaEndpoint"];
        var authResult = await _client.PostAsync(createDenunciaEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogInformation($"Ocorreu um erro durante a criação da denuncia: {authContent}");
            return null;
        }

        return await authResult.Content.ReadAsStringAsync();
    }

    public async Task<IList<DenunciaModel>?> GetAllDenuncias()
    {
        string getAllDenunciasEndpoint = _config["apiLocation"] + _config["getAllDenunciasEndpoint"];
        var authResult = await _client.GetAsync(getAllDenunciasEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogInformation($"Ocorreu um erro durante o carregamento das denuncias: {authContent}");
            return null;
        }

        var DenunciaModel = JsonConvert.DeserializeObject<IList<DenunciaModel>>(authContent);

        return DenunciaModel;
    }

    public async Task<string> DeleteDenuncia(Guid id)
    {
        string deleteDenunciaEndpoint = _config["apiLocation"] + _config["deleteDenunciaEndpoint"] + $"/{id}";
        var authResult = await _client.DeleteAsync(deleteDenunciaEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogInformation($"Ocorreu um erro para deletar a denuncia: {authContent}");
            return authContent;
        }

        return authContent;
    }
}
