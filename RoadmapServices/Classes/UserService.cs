using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RoadmapServices.Classes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
	private readonly IConfiguration _configuration;

	public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
		_configuration = configuration;
	}

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public async Task<UserModel?> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task AddUser(UserModel user)
    {
        bool verifyUser = VerifyIfUserExists(user);

        if (verifyUser is true)
        {
            throw new Exception("O nome de usuario ja esta cadastrado");
        }

        var implementUser = await ImplementUser(user);

        await _userRepository.AddUser(implementUser);
    }

    public Task UpdateUser(UserModel user)
    {
        return _userRepository.UpdateUser(user);
    }

    public Task DeleteUser(Guid id)
    {
        return _userRepository.DeleteUser(id);
    }

    public string Login(UserModel user)
    {
        bool verifyUser = VerifyIfUserExists(user);

        var requestedLogInUser = _userRepository.GetUserById(user.Id);

        bool verifyPassword = VerifyPasswordHash(
			requestedLogInUser.Result!.Password,
			requestedLogInUser.Result!.PasswordHash,
			requestedLogInUser.Result!.PasswordSalt);


        if (verifyUser is false) 
        {
            throw new Exception("Usuario ou senha incorretos");
        }

        if (verifyPassword is false)
        {
			throw new Exception("Usuario ou senha incorretos");
		}

        string token = CreateToken(requestedLogInUser.Result);

        return token;
	}


    private bool VerifyIfUserExists(UserModel user)
    {
		var users = _userRepository.GetAllUsers();

		foreach (var u in users.Result)
		{
			if (u.Username == user.Username)
			{
                return true;
			}
		}

        return false;
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

    private bool VerifyPasswordHash(
        string password,
        byte[] passwordHash,
        byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private async Task<UserModel> ImplementUser(UserModel user)
    {
        UserModel implementUser = new();

		CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

        implementUser.Id= user.Id;
		implementUser.Username = user.Username;
        implementUser.Password = user.Password;
        implementUser.PasswordHash = passwordHash;
        implementUser.PasswordSalt = passwordSalt;

        return implementUser;
    }

    private string CreateToken(UserModel user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
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
