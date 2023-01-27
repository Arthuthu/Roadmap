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
[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public AuthController(IMapper mapper, IUserService userService)
	{
		_userService = userService;
		_mapper = mapper;
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login(LoginRequest user)
	{
		var requestUser = _mapper.Map<UserModel>(user);
		string token = _userService.Login(requestUser);

		return Ok($"Token: {token}");
	}

	[HttpPost("register")]
	public async Task<ActionResult<List<UserResponse>>> Register(UserRequest user)
	{
		var requestUser = _mapper.Map<UserModel>(user);
		await _userService.AddUser(requestUser);

		return Ok(user);
	}
}
