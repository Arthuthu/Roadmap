using Microsoft.AspNetCore.Mvc;
using RoadmapRepository.Data;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserData _userData;

	public UserController(IUserData userData)
	{
		_userData = userData;
	}

	[HttpGet]
	public async Task<ActionResult<List<UserModel>>> GetAllUsers()
	{
		var users = await _userData.GetAllUsers();

		return Ok(users);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserModel>> GetUserById(Guid id)
	{
		var user = await _userData.GetUserById(id);

		return Ok(user);
	}

	[HttpPost]
	public async Task<ActionResult<List<UserModel>>> CreateUser(UserModel user)
	{
		await _userData.AddUser(user);

		return Ok(user);
	}

	[HttpPut]
	public async Task<ActionResult<List<UserModel>>> UpdateUser(UserModel requestedUser)
	{
		await _userData.UpdateUser(requestedUser);

		return Ok(requestedUser);
	}

	[HttpDelete]
	public async Task<ActionResult<UserModel>> DeleteUser(Guid id)
	{
		await _userData.DeleteUser(id);

		return Ok("User has been deleted");
	}
}
