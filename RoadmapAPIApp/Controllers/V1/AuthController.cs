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
[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IUserService _userService;
	private readonly IMessageHandler _messageHandler;

	public AuthController(IMapper mapper,
		IUserService userService,
		IMessageHandler messageHandler)
	{
		_userService = userService;
		_mapper = mapper;
		_messageHandler = messageHandler;
	}

	[Route("/login")]
	[HttpPost]
	public async Task<ActionResult> Login([FromForm] LoginRequest loginUser)
	{

		var requestUser = _mapper.Map<UserModel>(loginUser);
		var loginResponse = await _userService.Login(requestUser);

		string? token = loginResponse.FirstOrDefault()!;

		if (!token.StartsWith("ey"))
		{
			token = null;

			return BadRequest("Não foi possivel efetuar o login");
		}

		var output = new
		{
			Access_Token = token,
			Username = loginUser.Username
		};

		return Ok(output);
	}


	[Route("/register")]
	[HttpPost]
	public async Task<ActionResult<List<UserResponse>>> Register([FromForm] RegisterRequest registerRequest)
	{
		var requestUser = _mapper.Map<UserModel>(registerRequest);
		var registrationMessages = await _userService.AddUser(requestUser);
		var cleanResponses = await _messageHandler.ConcatRegistrationMessages(registrationMessages);

		return Ok(cleanResponses);
	}
}
