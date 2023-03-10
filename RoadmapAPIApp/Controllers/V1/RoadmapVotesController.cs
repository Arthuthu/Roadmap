using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Response;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class RoadmapVotesController : ControllerBase
{
	private readonly IRoadmapVotesService _roadmapVotesService;
	private readonly IMapper _mapper;
	private readonly IMessageHandler _messageHandler;

	public RoadmapVotesController(IRoadmapVotesService roadmapVotesService,
		IMapper mapper,
		IMessageHandler messaHandler)
	{
		_roadmapVotesService = roadmapVotesService;
		_mapper = mapper;
		_messageHandler = messaHandler;
	}

	[AllowAnonymous]
	[Route("/getallroadmapvotes")]
	[HttpGet]
	public async Task<ActionResult<List<RoadmapVotesResponse>>> GetAllRoadmapVotes()
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();
		var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));

		return Ok(responseRoadmapVotes);
	}

	[Route("/addroadmapvote/{userId}/{roadmapId}")]
	[HttpPost]
	public async Task<ActionResult<string>> CreateRoadmapVote(Guid userId, Guid roadmapId)
	{
		var roadmapVotingResponseMessage = await _roadmapVotesService.AddRoadmapVote(userId, roadmapId);

		return Ok(roadmapVotingResponseMessage);
	}

	[Route("/removeroadmapvote/{roadmapVoteId}")]
	[HttpDelete]
	public async Task<ActionResult<RoadmapClassResponse>> DeleteRoadmap(Guid roadmapVoteId)
	{
		await _roadmapVotesService.DeleteRoadmapVote(roadmapVoteId);

		return Ok("Vote has been removed");
	}
}
