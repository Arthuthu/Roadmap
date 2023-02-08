using AutoMapper;
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

	[HttpGet]
	public async Task<ActionResult<List<RoadmapVotesResponse>>> GetAllRoadmapVotes()
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();
		var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));

		return Ok(responseRoadmapVotes);
	}

	[HttpPost]
	public async Task<ActionResult<string>> CreateRoadmapVote(RoadmapVotesRequest roadmapVote)
	{
		var requestRoadmapVote = _mapper.Map<RoadmapVotesModel>(roadmapVote);
		var roadmapVotingResponseMessage = await _roadmapVotesService.AddRoadmapVote(requestRoadmapVote);

		return Ok(roadmapVotingResponseMessage);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<RoadmapClassResponse>> DeleteRoadmap(Guid id)
	{
		await _roadmapVotesService.DeleteRoadmapVote(id);

		return Ok("Vote has been removed");
	}
}
