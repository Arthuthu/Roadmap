using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapAPIApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoadmapController : ControllerBase
{
	private readonly IRoadmapService _roadmapService;
	private readonly IMapper _mapper;

	public RoadmapController(IRoadmapService roadmapService, IMapper mapper)
	{
		_roadmapService = roadmapService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<List<RoadmapResponse>>> GetAllRoadmaps()
	{
		var roadmaps = await _roadmapService.GetAllRoadmaps();
		var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapResponse>(roadmap));

		return Ok(responseRoadmaps);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<RoadmapResponse>> GetRoadmapById(Guid id)
	{
		var roadmap = await _roadmapService.GetRoadmapById(id);
		var responseRoadmaps = _mapper.Map<RoadmapResponse>(roadmap);

		return Ok(responseRoadmaps);
	}

	[HttpPost]
	public async Task<ActionResult<List<RoadmapResponse>>> CreateRoadmap(RoadmapRequest roadmap)
	{
		var requestRoadmap = _mapper.Map<RoadmapModel>(roadmap);
		await _roadmapService.AddRoadmap(requestRoadmap);

		return Ok(requestRoadmap);
	}

	[HttpPut]
	public async Task<ActionResult<List<RoadmapResponse>>> UpdateRoadmap(RoadmapRequest roadmap)
	{
		var requestRoadmap = _mapper.Map<RoadmapModel>(roadmap);
		await _roadmapService.UpdateRoadmap(requestRoadmap);

		return Ok(requestRoadmap);
	}

	[HttpDelete]
	public async Task<ActionResult<RoadmapResponse>> DeleteRoadmap(Guid id)
	{
		await _roadmapService.DeleteRoadmap(id);

		return Ok("Roadmap has been deleted");
	}
}
