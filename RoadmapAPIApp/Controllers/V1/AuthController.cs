using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using System;

namespace RoadmapAPIApp.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public AuthController(IMapper mapper,
		IUserService userService)
	{
		_userService = userService;
		_mapper = mapper;
	}

	[Route("/login")]
	[HttpPost]
	public async Task<ActionResult> Login([FromForm] LoginRequest loginUser)
	{
		var requestUser = _mapper.Map<UserModel>(loginUser);
		string token = _userService.Login(requestUser);

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
		var cleanResponses = await _userService.ConcatRegistrationMessages(registrationMessages);

		return Ok(cleanResponses);
	}
}
