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
public class RoadmapClassController : ControllerBase
{
    private readonly IRoadmapClassService _roadmapService;
    private readonly IMapper _mapper;
    private readonly IMessageHandler _messageHandler;

    public RoadmapClassController(IRoadmapClassService roadmapService,
        IMapper mapper,
        IMessageHandler messaHandler)
    {
        _roadmapService = roadmapService;
        _mapper = mapper;
        _messageHandler = messaHandler;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/getallroadmaps")]
    public async Task<ActionResult<List<RoadmapClassResponse>>> GetAllRoadmaps()
    {
        var roadmaps = await _roadmapService.GetAllRoadmaps();
        var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapClassResponse>(roadmap));

        return Ok(responseRoadmaps);
    }

	[HttpGet]
	[AllowAnonymous]
	[Route("/getallapprovedroadmaps")]
	public async Task<ActionResult<List<RoadmapClassResponse>>> GetAllApprovedRoadmaps()
	{
		var roadmaps = await _roadmapService.GetAllApprovedRoadmaps();
		var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapClassResponse>(roadmap));

		return Ok(responseRoadmaps);
	}

	[HttpGet]
	[Route("/getallnotapprovedroadmaps")]
	public async Task<ActionResult<List<RoadmapClassResponse>>> GetAllNotApprovedRoadmaps()
	{
		var roadmaps = await _roadmapService.GetAllNotApprovedRoadmaps();
		var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapClassResponse>(roadmap));

		return Ok(responseRoadmaps);
	}

	[Route("/getroadmapbyid/{id}")]
    [HttpGet]
    public async Task<ActionResult<RoadmapClassResponse>> GetRoadmapById(Guid id)
    {
        var roadmap = await _roadmapService.GetRoadmapById(id);
        var responseRoadmaps = _mapper.Map<RoadmapClassResponse>(roadmap);

        return Ok(responseRoadmaps);
    }

	[Route("/getroadmapbyuserid/{userId}")]
	[HttpGet]
	public async Task<ActionResult<RoadmapClassResponse>> GetRoadmapByUserId(Guid userId)
	{
		var roadmaps = await _roadmapService.GetRoadmapsByUserId(userId);
		var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapClassResponse>(roadmap));

		return Ok(responseRoadmaps);
	}

	[Route("/createroadmap")]
    [HttpPost]
    public async Task<ActionResult<List<RoadmapClassResponse>>> CreateRoadmap([FromForm] RoadmapClassRequest roadmap)
    {
        var requestRoadmap = _mapper.Map<RoadmapClassModel>(roadmap);
        var roadmapCreationMessages = await _roadmapService.AddRoadmap(requestRoadmap);
        var cleanResponses = _messageHandler.ConcatRegistrationMessages(roadmapCreationMessages);

        return Ok(cleanResponses);
    }

    [Route("/updateroadmap")]
    [HttpPut]
    public async Task<ActionResult<List<RoadmapClassResponse>>> UpdateRoadmap([FromForm] RoadmapClassRequest roadmap)
    {
        var requestRoadmap = _mapper.Map<RoadmapClassModel>(roadmap);
        await _roadmapService.UpdateRoadmap(requestRoadmap);

        return Ok(requestRoadmap);
    }

    [Route("/deletealluserroadmaps/{userId}")]
    [HttpDelete]
    public async Task<ActionResult<RoadmapClassResponse>> DeleteAllUserRoadmaps(Guid userId)
    {
        await _roadmapService.DeleteAllUserRoadmaps(userId);

        return Ok("Roadmaps do usuario foram deletados com sucesso");
    }

    [Route("/deleteroadmap/{id}")]
    [HttpDelete]
    public async Task<ActionResult<RoadmapClassResponse>> DeleteRoadmap(Guid id)
    {
        await _roadmapService.DeleteRoadmap(id);

        return Ok("Roadmap foi deletado com sucesso");
    }
}
