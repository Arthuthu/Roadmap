using Microsoft.AspNetCore.Mvc;
using RoadmapRepository.Models;
using RoadmapServices.User;

namespace RoadmapAPIApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpGet]
	public async Task<ActionResult<List<UserModel>>> GetAllUsers()
	{
		var users = await _userService.GetAllUsers();

		return Ok(users);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserModel>> GetUserById(Guid id)
	{
		var user = await _userService.GetUserById(id);

		return Ok(user);
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
