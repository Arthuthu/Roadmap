using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        var responseUsers = users.Select(user => _mapper.Map<UserResponse>(user));

        return Ok(responseUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        var responseUsers = _mapper.Map<UserResponse>(user);

        return Ok(responseUsers);
    }

    [HttpPost]
    public async Task<ActionResult<List<UserResponse>>> CreateUser(UserRequest user)
    {
        var requestUser = _mapper.Map<UserModel>(user);
        await _userService.AddUser(requestUser);

        return Ok(requestUser);
    }

    [HttpPut]
    public async Task<ActionResult<List<UserResponse>>> UpdateUser(UserRequest user)
    {
        var requestUser = _mapper.Map<UserModel>(user);
        await _userService.UpdateUser(requestUser);

        return Ok(requestUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserResponse>> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);

        return Ok("User has been deleted");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserRequest user)
    {
        var requestUser = _mapper.Map<UserModel>(user);
		string token = _userService.Login(requestUser);

        return Ok(token);
    }
}
