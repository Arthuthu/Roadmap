using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roadmap.API.Request;
using Roadmap.API.Response;
using Roadmap.Domain.Models;
using Roadmap.Infra.Interfaces;
using Roadmap.Infra.Validators.Interfaces;

namespace Roadmap.API.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class DenunciaController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDenunciaService _denunciaService;
    private readonly IMessageHandler _messageHandler;

    public DenunciaController(IMapper mapper,
        IDenunciaService denunciaService,
        IMessageHandler messageHandler)
    {
        _denunciaService = denunciaService;
        _mapper = mapper;
        _messageHandler = messageHandler;
    }

    [HttpGet]
    [Route("/getalldenuncias")]
    public async Task<ActionResult<List<DenunciaResponse>>> GetAllDenuncias()
    {
        var denuncias = await _denunciaService.GetAllDenuncias();
        var responseDenuncias = denuncias.Select(denuncias => _mapper.Map<DenunciaResponse>(denuncias));

        return Ok(responseDenuncias);
    }

    [Route("/getdenunciabyid/{id}")]
    [HttpGet]
    public async Task<ActionResult<DenunciaResponse>> GetDenunciaById(Guid id)
    {
        var denuncias = await _denunciaService.GetDenunciaById(id);
        var responseDenuncias = _mapper.Map<DenunciaResponse>(denuncias);

        return Ok(responseDenuncias);
    }

    [Route("/createdenuncia")]
    [HttpPost]
    public async Task<ActionResult<List<DenunciaResponse>>> CreateDenuncia([FromForm] DenunciaRequest denuncia)
    {
        var requestDenuncia = _mapper.Map<DenunciaModel>(denuncia);
        await _denunciaService.CreateDenuncia(requestDenuncia);

        return Ok(requestDenuncia);
    }

    [Route("/updatedenuncia")]
    [HttpPut]
    public async Task<ActionResult<List<DenunciaResponse>>> UpdateDenuncia([FromForm] DenunciaRequest denuncia)
    {
        var requestDenuncia = _mapper.Map<DenunciaModel>(denuncia);
        await _denunciaService.UpdateDenuncia(requestDenuncia);

        return Ok(requestDenuncia);
    }

    [Route("/deletedenuncia/{id}")]
    [HttpDelete]
    public async Task<ActionResult<DenunciaResponse>> DeleteRoadmap(Guid id)
    {
        await _denunciaService.DeleteDenuncia(id);

        return Ok("Denuncia foi deletada com sucesso");
    }
}
