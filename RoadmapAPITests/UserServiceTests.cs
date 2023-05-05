using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Routing;
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

	//Add user
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

	[Fact]
	public async Task AddUser_ShouldReturnError_WhenParametersAreInvalid()
	{
		// Arrange
		var userModel = _fixture.Build<UserModel>()
			.With(u => u.Username, "John")
			.With(u => u.Email, "john@hotmail.com")
			.With(u => u.Password, "john")
			.Create();

		var userValidator = new UserValidator();
		var validationResult = userValidator.TestValidate(userModel);

		_userRepository.GetUserByName(userModel.Username).Returns(Task.FromResult<UserModel?>(null));

		// Act
		var result = await _sut.AddUser(userModel);

		// Assert
		validationResult.ShouldHaveAnyValidationError();
	}

	//GetAllUsers

	[Fact]
	public async Task GetAllUsers_ShouldReturnAllUsers_WhenTheMethodIsCalled()
	{
		var expectedUsers = new List<UserModel> 
		{
			new UserModel { Id = Guid.NewGuid(), Username = "user1", Email = "user1@example.com" },
			new UserModel { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com" },
			new UserModel { Id = Guid.NewGuid(), Username = "user3", Email = "user3@example.com" }
		};

		_userRepository.GetAllUsers().Returns(Task.FromResult<IEnumerable<UserModel>>(expectedUsers));

		// Act
		var result = await _sut.GetAllUsers();

		// Assert
		result.Should().NotBeNull();
		result.Should().BeEquivalentTo(expectedUsers);
	}

	[Fact]
	public async Task GetAllUsers_ShouldReturnEmptyList_WhenThereAreNoUsers()
	{
		// Arrange
		var expectedUsers = Enumerable.Empty<UserModel>();
		_userRepository.GetAllUsers().Returns(Task.FromResult(expectedUsers));

		// Act
		var result = await _sut.GetAllUsers();

		// Assert
		result.Should().NotBeNull().And.BeEmpty();
	}

	//GetUserById

	[Fact]
	public async Task GetUserById_ShouldReturnUser_WhenIdParameterIsValid()
	{
		// Arrange
		var userId = Guid.NewGuid();
		UserModel? expectedUser = new UserModel 
		{ 
			Id = userId,
			Username = "John",
			Email = "john@hotmail.com",
			Password = "johnpassword123"
		};

		_userRepository.GetUserById(userId)!.Returns(Task.FromResult(expectedUser));

		//Act
		var result = await _sut.GetUserById(userId);

		// Assert
		result.Should().NotBeNull()
			.And.BeOfType<UserModel>()
			.And.BeSameAs(expectedUser);
	}

	[Fact]
	public async Task GetUserById_ShouldReturnNull_WhenUserNotFound()
	{
		// Arrange
		var userId = Guid.NewGuid();
		UserModel? expectedUser = null;

		_userRepository.GetUserById(userId).Returns(Task.FromResult(expectedUser));

		// Act
		var result = await _sut.GetUserById(userId);

		// Assert
		result.Should().BeNull();
	}

}
