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
public class NodeController : ControllerBase
{
    private readonly INodeService _nodeService;
    private readonly IMapper _mapper;

    public NodeController(INodeService nodeService, IMapper mapper)
    {
        _nodeService = nodeService;
        _mapper = mapper;
    }

	[Route("/getallnodes/{roadmapid}")]
	[HttpGet]
    public async Task<ActionResult<List<NodeResponse>>> GetAllNodes(Guid roadmapId)
    {
        var nodes = await _nodeService.GetAllNodes(roadmapId);
        var responseNodes = nodes.Select(node => _mapper.Map<NodeResponse>(node));

        return Ok(responseNodes);
    }

	[Route("/getbodebyid/{nodeId}")]
	[HttpGet]
    public async Task<ActionResult<NodeResponse>> GetNodeById(Guid nodeId)
    {
        var node = await _nodeService.GetNodeById(nodeId);
        var responseNodes = _mapper.Map<NodeResponse>(node);

        return Ok(responseNodes);
    }

	[Route("/createnode")]
	[HttpPost]
    public async Task<ActionResult<List<NodeResponse>>> CreateNode([FromForm] NodeRequest node)
    {
        var requestNode = _mapper.Map<NodeModel>(node);
        await _nodeService.AddNode(requestNode);

        return Ok(requestNode);
    }

    [Route("/updatenode")]
    [HttpPut]
    public async Task<ActionResult<List<NodeResponse>>> UpdateNode([FromForm] NodeRequest node)
    {
        var requestNode = _mapper.Map<NodeModel>(node);
        await _nodeService.UpdateNode(requestNode);

        return Ok(requestNode);
    }

	[Route("/deletenode/{nodeid}")]
	[HttpDelete]
    public async Task<ActionResult<NodeResponse>> DeleteNode(Guid nodeid)
    {
        await _nodeService.DeleteNode(nodeid);

        return Ok("Node has been deleted");
    }
}
