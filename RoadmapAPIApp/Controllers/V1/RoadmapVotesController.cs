using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roadmap.API.Response;
using Roadmap.Infra.Interfaces;
using Roadmap.Infra.Validators.Interfaces;

namespace Roadmap.API.Controllers.V1;

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
    [Route("/getallroadmapvotes/{userId}/{roadmapId}")]
    [HttpGet]
    public async Task<ActionResult<List<RoadmapVotesResponse>>> GetAllRoadmapVotes(Guid userId, Guid roadmapId)
    {
        var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes(userId, roadmapId);
        var responseRoadmapVotes = roadmapVotes.Select(roadmapVotes => _mapper.Map<RoadmapVotesResponse>(roadmapVotes));

        return Ok(responseRoadmapVotes);
    }

    [Route("/getallroadmapvotesbyuserid/{userId}")]
    [HttpGet]
    public async Task<ActionResult<string>> CreateRoadmapVote(Guid userId)
    {
        var roadmapVotingResponseMessage = await _roadmapVotesService.GetAllRoadmapVotesByUserId(userId);

        return Ok(roadmapVotingResponseMessage);
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
