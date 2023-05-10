using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Response;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace ComentarioAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class ComentarioVotesController : ControllerBase
{
	private readonly IComentarioVotesService _comentarioVotesService;
	private readonly IMapper _mapper;
	private readonly IMessageHandler _messageHandler;

	public ComentarioVotesController(IComentarioVotesService comentarioVotesService,
		IMapper mapper,
		IMessageHandler messaHandler)
	{
		_comentarioVotesService = comentarioVotesService;
		_mapper = mapper;
		_messageHandler = messaHandler;
	}

	[AllowAnonymous]
	[Route("/getallcomentariovotes/{userId}/{comentarioId}")]
	[HttpGet]
	public async Task<ActionResult<List<ComentarioVotesResponse>>> GetAllComentarioVotes(Guid userId, Guid comentarioId)
	{
		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes(userId, comentarioId);
		var responseComentarioVotes = comentarioVotes.Select(comentarioVotes => _mapper.Map<ComentarioVotesResponse>(comentarioVotes));

		return Ok(responseComentarioVotes);
	}

	[Route("/addcomentariovote/{userId}/{comentarioId}")]
	[HttpPost]
	public async Task<ActionResult<string>> CreateComentarioVote(Guid userId, Guid comentarioId)
	{
		var comentarioVotingResponseMessage = await _comentarioVotesService.AddComentarioVote(userId, comentarioId);

		return Ok(comentarioVotingResponseMessage);
	}

	[Route("/removecomentariovote/{comentarioVoteId}")]
	[HttpDelete]
	public async Task<ActionResult<ComentarioVotesResponse>> DeleteComentarioVote(Guid comentarioVoteId)
	{
		await _comentarioVotesService.DeleteComentarioVote(comentarioVoteId);

		return Ok("Vote has been removed");
	}
}
