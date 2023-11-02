using AutoMapper;
using Domain.Models;
using Infra.Interfaces;
using Infra.Validators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPI.Request;
using RoadmapAPI.Response;

namespace RoadmapAPI.Controllers.V1;

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
	[Route("/getallcomentariosbyroadmapid/{roadmapid}")]
	public async Task<ActionResult<List<ComentarioResponse>>> GetAllComentarios(Guid roadmapid)
	{
		var comentarios = await _comentarioService.GetAllComentarios(roadmapid);
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

	[Route("/deleteallusercomentarios/{userid}")]
	[HttpDelete]
	public async Task<ActionResult<ComentarioResponse>> DeleteAllUserComentarios(Guid userId)
	{
		await _comentarioService.DeleteAllUserComentarios(userId);

		return Ok("Comentario foi deletado com sucesso");
	}

	[Route("/deletecomentario/{id}")]
	[HttpDelete]
	public async Task<ActionResult<ComentarioResponse>> DeleteRoadmap(Guid id)
	{
		await _comentarioService.DeleteComentario(id);

		return Ok("Comentario foi deletado com sucesso");
	}
}
