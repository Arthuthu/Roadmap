using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Dtos;
using RoadmapRepository.Models;
using RoadmapServices.User;

namespace RoadmapAPIApp.Controllers;

[Route("api/[controller]")]
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
	public async Task<ActionResult<List<UserDto>>> GetAllUsers()
	{
		var users = await _userService.GetAllUsers();
		var dtoUsers = users.Select(user => _mapper.Map<UserDto>(user));

		return Ok(dtoUsers);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserDto>> GetUserById(Guid id)
	{
		var user = await _userService.GetUserById(id);
		var dtoUser = _mapper.Map<UserDto>(user);

		return Ok(dtoUser);
	}

	[HttpPost]
	public async Task<ActionResult<List<UserModel>>> CreateUser(UserModel user)
	{
		await _userService.AddUser(user);

		return Ok(user);
	}

	[HttpPut]
	public async Task<ActionResult<List<UserModel>>> UpdateUser(UserModel requestedUser)
	{
		await _userService.UpdateUser(requestedUser);

		return Ok(requestedUser);
	}

	[HttpDelete]
	public async Task<ActionResult<UserModel>> DeleteUser(Guid id)
	{
		await _userService.DeleteUser(id);

		return Ok("User has been deleted");
	}
}
