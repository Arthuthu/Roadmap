﻿using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
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

	public async Task<UserModel?> GetUserByConfirmationCode(Guid confirmationCode)
	{
		return await _userRepository.GetUserByConfirmationCode(confirmationCode);
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
	public Task DeleteUser(Guid id)
	{
		return _userRepository.DeleteUser(id);
	}

	public async Task<IList<string?>> AddUser(UserModel user)
    {
		IList<string?> registrationMessages = new List<string?>();

		registrationMessages = await UserRegistrationVerifications(user);

		if (registrationMessages.Count != 0)
		{
			return registrationMessages;
		}

        var createdUser = InsertUserData(user);

		try
		{
			await _userRepository.AddUser(createdUser);
			await SendEmail(createdUser);
			registrationMessages.Add("Usuario registrado com sucesso");
		}
		catch (Exception ex)
		{
			registrationMessages.Add($"Ocorreu um erro durante o registro de usuario {ex.Message}");
		}

		return registrationMessages;
    }

    public async Task<IEnumerable<string?>> Login(UserModel user)
    {
		IList<string?> loginVerifications = new List<string?>();

		loginVerifications = await UserLoginVerifications(user);

		loginVerifications.Add(CreateToken(user));

		var loginReturn = loginVerifications.Where(x => x != null);

        return loginReturn;
	}

	//Verifications
	private async Task<IList<string?>> UserRegistrationVerifications(UserModel user)
	{
		IList<string?> verificationMessages = new List<string?>();

		verificationMessages.Add(await VerifyIfUserAlreadyExists(user));
		verificationMessages.Add(await VerifyIfEmailIsRegistered(user));

		var fluentValidationMessages = await _messageHandler.ValidateUserRegistration(user);

		foreach (var message in fluentValidationMessages)
		{
			verificationMessages.Add(message);
		}

		var cleanMessages = verificationMessages.Where(x => !x.IsNullOrEmpty()).ToList();

		return cleanMessages;
	}

	private async Task<IList<string?>> UserLoginVerifications(UserModel user)
	{
		IList<string?> loginMessages = new List<string?>();

		loginMessages.Add(await VerifyIfUsernameIsCorrect(user));
		loginMessages.Add(await VerifyIfPasswordIsCorrect(user));

		var requestedLogInUser = await _userRepository.GetUserByName(user);

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
		var requestedUser = await _userRepository.GetUserByName(user);

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
		var requestedUser = await _userRepository.GetUserByName(user);

		if (requestedUser is not null)
		{
			return null;
		}

		return "Usuario ou senha incorretos";
	}

	private async Task<string?> VerifyIfPasswordIsCorrect(UserModel user)
	{
		var requestedUser = await _userRepository.GetUserByName(user);

        if (requestedUser!.Password != user.Password)
        {
            return "Usuario ou senha incorretos";
        }

		return null;
	}

	private string? VerifyPasswordHash(
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

    private UserModel InsertUserData(UserModel user)
    {
		CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

		user.Id = Guid.NewGuid();
		user.PasswordHash = passwordHash;
		user.PasswordSalt = passwordSalt;
		user.ConfirmationCode = Guid.NewGuid();
		user.ConfirmationCodeExpirationDate = DateTime.UtcNow.AddDays(1).AddHours(-3);
		user.CreatedDate = DateTime.UtcNow.AddHours(-3);

        return user;
    }

	private async Task SendEmail(UserModel user)
	{
			var sender = new SmtpSender(() => new SmtpClient("localhost")
			{
				DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
				PickupDirectoryLocation = @"C:\Users\User\Desktop\Emails"
			});

			Email.DefaultSender = sender;

			var email = await Email
				.From("arthurgeromello@hotmail.com")
				.To("arthur_geromello@hotmail.com")
				.Subject("Roadmap Email Confirmation")
				.Body($"Clique no link para confirmar o seu email: https://localhost:7290/emailconfirmation/{user.ConfirmationCode}")
				.SendAsync();
	}

    private string CreateToken(UserModel user)
    {
		List<Claim> claims = new List<Claim>
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
