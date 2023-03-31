using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]

public class ComentarioController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IComentarioService _comentarioService;
	private readonly IMessageHandler _messageHandler;

	public ComentarioController(IMapper mapper,
		IComentarioService comentarioService,
		IMessageHandler messageHandler)
	{
		_comentarioService = comentarioService;
		_mapper = mapper;
		_messageHandler = messageHandler;
	}

	[HttpGet]
	[AllowAnonymous]
	[Route("/getallcomentarios")]
	public async Task<ActionResult<List<ComentarioResponse>>> GetAllComentarios()
	{
		var comentarios = await _comentarioService.GetAllComentarios();
		var responseComentarios = comentarios.Select(comentarios => _mapper.Map<ComentarioResponse>(comentarios));

		return Ok(responseComentarios);
	}

	[Route("/getcomentariobyid/{id}")]
	[HttpGet]
	public async Task<ActionResult<ComentarioResponse>> GetComentarioById(Guid id)
	{
		var comentarios = await _comentarioService.GetComentarioById(id);
		var responseComentarios = _mapper.Map<ComentarioResponse>(comentarios);

		return Ok(responseComentarios);
	}

	[Route("/createcomentario")]
	[HttpPost]
	public async Task<ActionResult<List<ComentarioResponse>>> CreateComentario([FromForm] ComentarioRequest comentario)
	{
		var requestComentario = _mapper.Map<ComentarioModel>(comentario);
		await _comentarioService.CreateComentario(requestComentario);

		return Ok(requestComentario);
	}

	[Route("/updatecomentario")]
	[HttpPut]
	public async Task<ActionResult<List<ComentarioResponse>>> UpdateComentario([FromForm] ComentarioRequest comentario)
	{
		var requestComentario = _mapper.Map<ComentarioModel>(comentario);
		await _comentarioService.UpdateComentario(requestComentario);

		return Ok(requestComentario);
	}

	[Route("/deletecomentario/{id}")]
	[HttpDelete]
	public async Task<ActionResult<ComentarioResponse>> DeleteRoadmap(Guid id)
	{
		await _comentarioService.DeleteComentario(id);

		return Ok("Comentario foi deletado com sucesso");
	}
}
