using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;
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
	public void AddUser_ShouldCreateUser_WhenAllParametersAreValid()
	{
		//Arrange
		UserModel user = new UserModel();
		user.Username = "John";
		user.Password = "johnpassword123";


		//Act
		var result = _sut.AddUser(user);

		//Assert
		result.Result.Should().Contain("Usuario registrado com sucesso");
	}
}
