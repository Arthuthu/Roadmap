using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class RoadmapClassController : ControllerBase
{
    private readonly IRoadmapClassService _roadmapService;
    private readonly IMapper _mapper;

    public RoadmapClassController(IRoadmapClassService roadmapService, IMapper mapper)
    {
        _roadmapService = roadmapService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoadmapClassResponse>>> GetAllRoadmaps()
    {
        var roadmaps = await _roadmapService.GetAllRoadmaps();
        var responseRoadmaps = roadmaps.Select(roadmap => _mapper.Map<RoadmapClassResponse>(roadmap));

        return Ok(responseRoadmaps);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoadmapClassResponse>> GetRoadmapById(Guid id)
    {
        var roadmap = await _roadmapService.GetRoadmapById(id);
        var responseRoadmaps = _mapper.Map<RoadmapClassResponse>(roadmap);

        return Ok(responseRoadmaps);
    }

    [Route("/createroadmap")]
    [HttpPost]
    public async Task<ActionResult<List<RoadmapClassResponse>>> CreateRoadmap([FromForm] RoadmapClassRequest roadmap)
    {
        var requestRoadmap = _mapper.Map<RoadmapClassModel>(roadmap);
        await _roadmapService.AddRoadmap(requestRoadmap);

        return Ok("Roadmap criado com sucesso");
    }

    [HttpPut]
    public async Task<ActionResult<List<RoadmapClassResponse>>> UpdateRoadmap(RoadmapClassRequest roadmap)
    {
        var requestRoadmap = _mapper.Map<RoadmapClassModel>(roadmap);
        await _roadmapService.UpdateRoadmap(requestRoadmap);

        return Ok(requestRoadmap);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RoadmapClassResponse>> DeleteRoadmap(Guid id)
    {
        await _roadmapService.DeleteRoadmap(id);

        return Ok("Roadmap has been deleted");
    }
}
