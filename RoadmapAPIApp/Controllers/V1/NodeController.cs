using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class NodeController : ControllerBase
{
    private readonly INodeService _nodeService;
    private readonly IMapper _mapper;

    public NodeController(INodeService nodeService, IMapper mapper)
    {
        _nodeService = nodeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<NodeResponse>>> GetAllNodes()
    {
        var nodes = await _nodeService.GetAllNodes();
        var responseNodes = nodes.Select(node => _mapper.Map<NodeResponse>(node));

        return Ok(responseNodes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NodeResponse>> GetNodeById(Guid id)
    {
        var node = await _nodeService.GetNodeById(id);
        var responseNodes = _mapper.Map<NodeResponse>(node);

        return Ok(responseNodes);
    }

    [HttpPost]
    public async Task<ActionResult<List<NodeResponse>>> CreateNode(NodeRequest node)
    {
        var requestNode = _mapper.Map<NodeModel>(node);
        await _nodeService.AddNode(requestNode);

        return Ok(requestNode);
    }

    [HttpPut]
    public async Task<ActionResult<List<NodeResponse>>> UpdateNode(NodeRequest node)
    {
        var requestNode = _mapper.Map<NodeModel>(node);
        await _nodeService.UpdateNode(requestNode);

        return Ok(requestNode);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<NodeResponse>> DeleteNode(Guid id)
    {
        await _nodeService.DeleteNode(id);

        return Ok("Node has been deleted");
    }
}
