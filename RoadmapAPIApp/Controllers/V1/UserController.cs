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
    [AllowAnonymous]
    [Route("/getallusers")]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        var responseUsers = users.Select(user => _mapper.Map<UserResponse>(user));

        return Ok(responseUsers);
    }

    [Route("/getuserbyid/{id}")]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        var responseUsers = _mapper.Map<UserResponse>(user);

        return Ok(responseUsers);
    }

    [Route("/getuserbyname/{username}")]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUserByName(string username)
    {
        var user = await _userService.GetUserByName(username);
        var responseUsers = _mapper.Map<UserResponse>(user);

        return Ok(responseUsers);
    }

    [Route("/getuserbyconfirmationcode/{confirmationcode}")]
	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<UserResponse>> GetUserByConfirmationCode(Guid confirmationCode)
	{
		var user = await _userService.GetUserByConfirmationCode(confirmationCode);
		var responseUsers = _mapper.Map<UserResponse>(user);

		return Ok(responseUsers);
	}

	[Route("/getuserbyrestorationcode/{restorationcode}")]
	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<UserResponse>> GetUserByRestorationCode(Guid restorationCode)
	{
		var user = await _userService.GetUserByRestorationCode(restorationCode);
		var responseUsers = _mapper.Map<UserResponse>(user);

		return Ok(responseUsers);
	}

	[HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<List<UserResponse>>> CreateUser(UserRequest user)
    {
        var requestUser = _mapper.Map<UserModel>(user);
        await _userService.AddUser(requestUser);

        return Ok(requestUser);
    }

    [Route("/updateuser")]
    [HttpPut]
    public async Task<ActionResult<List<UserResponse>>> UpdateUser([FromForm] UserRequest user)
    {
        var requestUser = _mapper.Map<UserModel>(user);
        await _userService.UpdateUser(requestUser);

        return Ok("Perfil atualizado com sucesso");
    }

	[Route("/updateuseremailconfirmation")]
	[HttpPut]
    [AllowAnonymous]
	public async Task<ActionResult<List<UserResponse>>> UpdateUserEmailConfirmation([FromForm] UserRequest user)
	{
		var requestUser = _mapper.Map<UserModel>(user);
		await _userService.UpdateUserEmailConfirmation(requestUser);

		return Ok("Confirmação de email atualizada com sucesso");
	}

	[Route("/sendrestorationemail")]
	[HttpPut]
	[AllowAnonymous]
	public async Task<ActionResult<List<UserResponse>>> SendRestorationEmail([FromForm] UserRequest user)
	{
		var requestUser = _mapper.Map<UserModel>(user);
		await _userService.SendRestorationEmail(requestUser);

		return Ok("Email enviado para restauração da conta");
	}

	[HttpDelete("{id}")]
    public async Task<ActionResult<UserResponse>> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);

        return Ok("User has been deleted");
    }
}
