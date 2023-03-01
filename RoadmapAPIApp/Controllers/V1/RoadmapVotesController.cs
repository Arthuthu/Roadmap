using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
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

	[Route("/getallroadmapvotes")]
	[HttpGet]
	public async Task<ActionResult<List<RoadmapVotesResponse>>> GetAllRoadmapVotes()
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();
		var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));

		return Ok(responseRoadmapVotes);
	}

	[Route("/getallroadmapsuservoted/{userId}")]
	[HttpGet]
	public async Task<ActionResult<List<RoadmapVotesResponse>>> GetAllRoadmapsUserVoted(Guid userId)
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapsUserVoted(userId);
		var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));

		return Ok(responseRoadmapVotes);
	}

	[Route("/getroadmapvotedidbyuserandroadmapid/{userId}/{roadmapId}")]
	[HttpGet]
	public async Task<ActionResult<RoadmapVotesResponse>> GetRoadmapVoteIdByUserAndRoadmapId(Guid userId, Guid roadmapId)
	{
		var votedRoadmap = await _roadmapVotesService.GetRoadmapVoteIdByUserAndRoadmapId(userId, roadmapId);
		var responseRoadmapVotes = _mapper.Map<RoadmapVotesResponse>(votedRoadmap);

		return Ok(responseRoadmapVotes);
	}

	[AllowAnonymous]
	[Route("/getroadmapvotesbyroadmapid/{roadmapId}")]
	[HttpGet]
	public async Task<ActionResult<RoadmapVotesResponse>> GetRoadmapVotesByRoadmapId(Guid roadmapId)
	{
		var roadmapVotes = await _roadmapVotesService.GetRoadmapVotesByRoadmapId(roadmapId);
		var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));


		return Ok(responseRoadmapVotes);
	}

	[Route("/addroadmapvote")]
	[HttpPost]
	public async Task<ActionResult<string>> CreateRoadmapVote([FromForm] RoadmapVotesRequest roadmapVote)
	{
		var requestRoadmapVote = _mapper.Map<RoadmapVotesModel>(roadmapVote);
		var roadmapVotingResponseMessage = await _roadmapVotesService.AddRoadmapVote(requestRoadmapVote);

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
