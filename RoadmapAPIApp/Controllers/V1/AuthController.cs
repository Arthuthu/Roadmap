using AutoMapper;
using Domain.Models;
using Infra.Interfaces;
using Infra.Validators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPI.Request;
using RoadmapAPI.Response;

namespace RoadmapAPI.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IUserService _userService;
	private readonly IMessageHandler _messageHandler;
	private const string tokenStartingLetters = "ey";

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

		if (!token.StartsWith(tokenStartingLetters))
		{
			return BadRequest("Não foi possivel efetuar o login");
		}

		var output = new
		{
			Access_Token = token,
			loginUser.Username
		};

		return Ok(output);
	}


	[Route("/register")]
	[HttpPost]
	public async Task<ActionResult<List<UserResponse>>> Register([FromForm] RegisterRequest registerRequest)
	{
		var requestUser = _mapper.Map<UserModel>(registerRequest);
		var registrationMessages = await _userService.AddUser(requestUser);
		var cleanResponses = _messageHandler.ConcatRegistrationMessages(registrationMessages!);

		return Ok(cleanResponses);
	}
}
