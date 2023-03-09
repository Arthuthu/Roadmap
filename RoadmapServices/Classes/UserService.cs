using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RoadmapServices.Classes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
	private readonly IConfiguration _configuration;
	private readonly IMessageHandler _messageHandler;

	public UserService(IUserRepository userRepository,
		IConfiguration configuration,
		IMessageHandler messageHandler)
    {
        _userRepository = userRepository;
		_configuration = configuration;
		_messageHandler = messageHandler;
	}

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public async Task<UserModel?> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

	public async Task<UserModel?> GetUserByName(UserModel user)
	{
		return await _userRepository.GetUserByName(user);
	}

	public async Task<IList<string>> AddUser(UserModel user)
    {
		IList<string> registrationMessages = new List<string>();

		bool verifyUser = await VerifyIfUserAlreadyExists(user);

		if (verifyUser is true)
        {
			registrationMessages.Add("Usuario ja esta cadastrado");
			return registrationMessages;
        }

		registrationMessages = await _messageHandler.ValidateUserRegistration(user);

		if (registrationMessages.Count != 0)
		{
			return registrationMessages;
		}

        var createdUser = await InsertUserIdPasswordHashSaltAndCreatedTime(user);

		try
		{
			await _userRepository.AddUser(createdUser);
			registrationMessages.Add("Usuario registrado com sucesso");
		}
		catch (Exception ex)
		{
			registrationMessages.Add($"Ocorreu um erro durante o registro de usuario {ex.Message}");
		}

		return registrationMessages;
    }

    public Task UpdateUser(UserModel user)
    {
		user.UpdatedDate = DateTime.UtcNow.AddHours(-3);

        return _userRepository.UpdateUser(user);
    }

    public Task DeleteUser(Guid id)
    {
        return _userRepository.DeleteUser(id);
    }

    public async Task<string> Login(UserModel user)
    {
        bool verifyUser = await VerifyIfUserAlreadyExists(user);

		if (verifyUser is false)
		{
			throw new Exception("Usuario ou senha incorretos");
		}

		bool verifyPassword = await VerifyIfPasswordIsCorrect(user);

		if (verifyPassword is false)
		{
			throw new Exception("Usuario ou senha incorretos");
		}

		var requestedLogInUser = await _userRepository.GetUserByName(user);

		bool verifyPasswordHash = await VerifyPasswordHash(
			requestedLogInUser.Password,
			requestedLogInUser.PasswordHash,
			requestedLogInUser.PasswordSalt);

		if (verifyPasswordHash is false)
		{
			throw new Exception("Ocorreu um erro durante o login");
		}

		string token = await CreateToken(requestedLogInUser);

        return token;
	}


    private async Task<bool> VerifyIfUserAlreadyExists(UserModel user)
    {
		var users = await _userRepository.GetAllUsers();

		foreach (var u in users)
		{
			if (u.Username == user.Username)
			{
                return true;
			}
		}

        return false;
	}

	private async Task<bool> VerifyIfPasswordIsCorrect(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByName(user);

        if (requestedUser.Password == user.Password)
        {
            return true;
        }

		return false;
	}

	private async Task<bool> VerifyPasswordHash(
		string password,
		byte[] passwordHash,
		byte[] passwordSalt)
	{
		using (var hmac = new HMACSHA512(passwordSalt))
		{
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			return computedHash.SequenceEqual(passwordHash);
		};
	}

	private void CreatePasswordHash(
		string password,
		out byte[] passwordHash,
		out byte[] passwordSalt)
	{
		using (var hmac = new HMACSHA512())
		{
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
		}
	}

    private async Task<UserModel> InsertUserIdPasswordHashSaltAndCreatedTime(UserModel user)
    {
		CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

		user.Id = Guid.NewGuid();
		user.PasswordHash = passwordHash;
		user.PasswordSalt = passwordSalt;
		user.CreatedDate = DateTime.UtcNow.AddHours(-3);

        return user;
    }

    private async Task<string> CreateToken(UserModel user)
    {
		List<Claim> claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Role, "User")
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
