using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;
using RoadmapServices.Validators;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapAPITests;

public class UserServiceTests
{
	private readonly UserService _sut;
	private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
	private readonly IConfiguration _config = Substitute.For<IConfiguration>();
	private readonly IMessageHandler _messageHandler = Substitute.For<IMessageHandler>();
	private readonly IFixture _fixture = new Fixture();

	public UserServiceTests()
	{
		_sut = new UserService(
			_userRepository,
			_config,
			_messageHandler);
	}

	[Fact]
	public async Task AddUser_ShoudlReturnSuccessMessage_WhenAllParametersAreCorrect()
	{
		// Arrange
		var userModel = _fixture.Build<UserModel>()
			.With(u => u.Username, "John")
			.With(u => u.Email, "john@hotmail.com")
			.With(u => u.Password, "johnpassword123")
			.Create();

		var userValidator = new UserValidator();
		var validationResult = userValidator.TestValidate(userModel);

		_userRepository.GetUserByName(userModel.Username).Returns(Task.FromResult<UserModel?>(null));

		// Act
		var result = await _sut.AddUser(userModel);

		// Assert
		validationResult.ShouldNotHaveAnyValidationErrors();

		result.Should().NotBeNull()
			.And.HaveCount(1)
			.And.Contain("Usuario registrado com sucesso");
	}
}
