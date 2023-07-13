using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
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

	public async Task<UserModel?> GetUserByName(string username)
	{
		return await _userRepository.GetUserByName(username);
	}

	public async Task<UserModel?> GetUserByConfirmationCode(Guid confirmationCode)
	{
		return await _userRepository.GetUserByConfirmationCode(confirmationCode);
	}

	public async Task<UserModel?> GetUserByRestorationCode(Guid restorationCode)
	{
		return await _userRepository.GetUserByRestorationCode(restorationCode);
	}
	public Task UpdateUser(UserModel user)
	{
		user.UpdatedDate = DateTime.UtcNow.AddHours(-3);

		return _userRepository.UpdateUser(user);
	}

	public Task UpdateUserEmailConfirmation(UserModel user)
	{
		user.UpdatedDate = DateTime.UtcNow.AddHours(-3);

		return _userRepository.UpdateUserEmailConfirmation(user);
	}

	public Task UpdateUserPassword(UserModel user)
	{
		user.UpdatedDate = DateTime.UtcNow.AddHours(-3);

		return _userRepository.UpdateUserPassword(user);
	}
	public Task DeleteUser(Guid id)
	{
		return _userRepository.DeleteUser(id);
	}

	public async Task<IList<string>?> AddUser(UserModel user)
    {
		IList<string>? registrationMessages = await UserRegistrationVerifications(user);

		if (registrationMessages is not null)
		{
			return registrationMessages;
		}

        var createdUser = InsertUserData(user);

		try
		{
			await _userRepository.AddUser(createdUser);
			await SendConfirmationEmail(createdUser);
			registrationMessages!.Add("Usuario registrado com sucesso");
		}
		catch (Exception ex)
		{
			registrationMessages!.Add($"Ocorreu um erro durante o registro de usuario {ex.Message}");
		}

		return registrationMessages;
    }

    public async Task<IEnumerable<string?>> Login(UserModel user)
    {
		IList<string?> loginVerifications = new List<string?>();

		loginVerifications = await UserLoginVerifications(user);

		var requestedUser = await GetUserByName(user.Username);

		if (requestedUser is null)
		{
			loginVerifications.Add("Usuario não encontrado");

			return loginVerifications;
		}

		loginVerifications.Add(CreateToken(requestedUser));

		var loginReturn = loginVerifications.Where(x => x != null);

        return loginReturn;
	}

	//Verifications
	private async Task<IList<string>?> UserRegistrationVerifications(UserModel user)
	{
		IList<string>? verificationMessages = new List<string>();

		string? userAlreadyExistsMessage = await VerifyIfUserAlreadyExists(user);
		if(userAlreadyExistsMessage is not null)
		{
			verificationMessages.Add(userAlreadyExistsMessage);
		}

		string? emailIsAlreadyRegisteredMessage = await VerifyIfEmailIsRegistered(user);
		if (emailIsAlreadyRegisteredMessage is not null)
		{
			verificationMessages.Add(emailIsAlreadyRegisteredMessage);
		}

		var fluentValidationMessages = _messageHandler.ValidateUserRegistration(user);

		if (fluentValidationMessages is not null)
		{
			foreach (var message in fluentValidationMessages)
			{
				verificationMessages.Add(message);
			}
		}

		var cleanMessages = verificationMessages.Where(x => !x.IsNullOrEmpty()).ToList();

		return cleanMessages is not null ? cleanMessages : null;
	}

	private async Task<IList<string?>> UserLoginVerifications(UserModel user)
	{
		IList<string?> loginMessages = new List<string?>
		{
			await VerifyIfUsernameIsCorrect(user),
			await VerifyIfPasswordIsCorrect(user)
		};

		var requestedLogInUser = await _userRepository.GetUserByName(user.Username);

		if (requestedLogInUser is null)
		{
			loginMessages.Add("Ocorreu ao encontrar o perfil de usuário");

			return loginMessages;
		}

		loginMessages.Add(VerifyPasswordHash(
			requestedLogInUser!.Password,
			requestedLogInUser.PasswordHash,
			requestedLogInUser.PasswordSalt));

		return loginMessages;
	}

    private async Task<string?> VerifyIfUserAlreadyExists(UserModel user)
    {
		var requestedUser = await _userRepository.GetUserByName(user.Username);

		if (requestedUser is not null)
		{
			return "Usuario ja esta cadastrado";
		}

        return null;
	}

	private async Task<string?> VerifyIfEmailIsRegistered(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByEmail(user);

		if (requestedUser is not null)
		{
			return "Email ja esta registrado";
		}

		return null;
	}

	private async Task<string?> VerifyIfUsernameIsCorrect(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByName(user.Username);

		if (requestedUser is not null)
		{
			return null;
		}

		return "Usuario ou senha incorretos";
	}

	private async Task<string?> VerifyIfPasswordIsCorrect(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByName(user.Username);

        if (requestedUser!.Password != user.Password)
        {
            return "Usuario ou senha incorretos";
        }

		return null;
	}

	private static string? VerifyPasswordHash(
		string? password,
		byte[]? passwordHash,
		byte[]? passwordSalt)
	{
		if (password is not null && passwordHash is not null  && passwordSalt is not null)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				var matchPassword = computedHash.SequenceEqual(passwordHash);

				if (matchPassword is true)
				{
					return null;
				}
			};
		}

		return "Ocorreu um erro durante o login";
	}

	//Data Creation
	private static void CreatePasswordHash(
		string password,
		out byte[] passwordHash,
		out byte[] passwordSalt)
	{
		using var hmac = new HMACSHA512();
		passwordSalt = hmac.Key;
		passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
	}

	private static UserModel InsertUserData(UserModel user)
    {
		CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

		user.Id = Guid.NewGuid();
		user.PasswordHash = passwordHash;
		user.PasswordSalt = passwordSalt;
		user.CreatedDate = DateTime.UtcNow.AddHours(-3);

        return user;
    }

	private async Task<UserModel?> InsertConfirmationData(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByEmail(user);

		if (requestedUser is not null)
		{
			requestedUser.ConfirmationCode = Guid.NewGuid();
			requestedUser.ConfirmationCodeExpirationDate = DateTime.UtcNow.AddDays(1)
				.AddHours(-3);

			await _userRepository.UpdateUser(requestedUser);

			return requestedUser;
		}

		return null;
	}

	private async Task<UserModel?> InsertRestorationData(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByEmail(user);

		if (requestedUser is not null)
		{
			requestedUser.RestorationCode = Guid.NewGuid();
			requestedUser.RestorationCodeExpirationDate = DateTime.UtcNow.AddDays(1)
                .AddHours(-3);

			await _userRepository.UpdateUser(requestedUser);

			return requestedUser;
		}

		return null;
	}

	public async Task SendConfirmationEmail(UserModel user)
	{
		try
		{
			var updatedUser = await InsertConfirmationData(user);

			if (updatedUser is not null)
			{
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse($"{_configuration.GetSection("EmailCredentials:Email").Value}"));
				//Alterar para user.Email
				email.To.Add(MailboxAddress.Parse($"{_configuration.GetSection("EmailCredentials:Email").Value}"));
				email.Subject = "Roadmap Email Confirmation";
				email.Body = new TextPart(TextFormat.Html)
				{
					Text = $"Clique no link para confirmar seu email: " +
					$"{_configuration.GetSection("SiteUrl").Value}/emailconfirmation/{updatedUser.ConfirmationCode}"
				};

				using var smtp = new SmtpClient();
				smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
				smtp.Authenticate($"{_configuration.GetSection("EmailCredentials:Email").Value}",
					$"{_configuration.GetSection("EmailCredentials:Password").Value}");
				smtp.Send(email);
				smtp.Disconnect(true);
			}
		}
		catch (Exception ex)
		{
			throw new Exception($"Ocorreu um erro ao enviar o email de confirmação, {ex.Message}");
		}
		
	}

	public async Task SendRestorationEmail(UserModel user)
	{
		try
		{
			var updatedUser = await InsertRestorationData(user);

			if (updatedUser is not null)
			{
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse($"{_configuration.GetSection("EmailCredentials:Email").Value}"));
				//Alterar para user.Email
				email.To.Add(MailboxAddress.Parse($"{_configuration.GetSection("EmailCredentials:Email").Value}"));
				email.Subject = "Roadmap Restauracao de Conta";
				email.Body = new TextPart(TextFormat.Html)
				{
					Text = $"Clique no link para mudar a sua senha: " +
					$"{_configuration.GetSection("SiteUrl").Value}/passwordchange/{updatedUser.RestorationCode}"
				};

				using var smtp = new SmtpClient();
				smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
				smtp.Authenticate($"{_configuration.GetSection("EmailCredentials:Email").Value}",
					$"{_configuration.GetSection("EmailCredentials:Password").Value}");
				smtp.Send(email);
				smtp.Disconnect(true);
			}
		}
		catch (Exception ex)
		{
			throw new Exception($"Ocorreu um erro para restaurar a conta, {ex.Message}");
		}
	}

	private string CreateToken(UserModel user)
    {
		List<Claim> claims = new()
		{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
			new Claim(ClaimTypes.Role, "User"),
			new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(2).ToString())
		};


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
			_configuration.GetSection("AppSettings:Token").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(2),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
