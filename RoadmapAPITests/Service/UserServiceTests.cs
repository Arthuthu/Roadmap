using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Roadmap.Domain.Interfaces;
using Roadmap.Domain.Models;
using Roadmap.Infra.Services;
using Roadmap.Infra.Validators;
using Roadmap.Infra.Validators.Interfaces;

namespace Roadmap.Tests.Service;

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
    public async Task AddUser_ShouldReturnSuccessMessage_WhenAllParametersAreCorrect()
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
    public async Task GetAllUsers_ShouldReturnAllUsers_WhenThereIsUsers()
    {
        var expectedUsers = new List<UserModel>
        {
            new UserModel { Id = Guid.NewGuid(), Username = "user1", Email = "user1@example.com" },
            new UserModel { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com" },
            new UserModel { Id = Guid.NewGuid(), Username = "user3", Email = "user3@example.com" }
        };

        _userRepository.GetAllUsers().Returns(expectedUsers);

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
        _userRepository.GetAllUsers().Returns(expectedUsers);

        // Act
        var result = await _sut.GetAllUsers();

        // Assert
        result.Should().NotBeNull().And.BeEmpty();
    }

    //GetUserById

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenIdIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();

        UserModel? expectedUser = _fixture.Build<UserModel>()
            .With(u => u.Id, userId)
            .With(u => u.Username, "John")
            .With(u => u.Email, "john@hotmail.com")
            .Create();

        _userRepository.GetUserById(userId)!.Returns(expectedUser);

        //Act
        var result = await _sut.GetUserById(userId);

        // Assert
        result.Should().NotBeNull()
            .And.BeOfType<UserModel>()
            .And.BeSameAs(expectedUser);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNull_WhenThereAreNoUsers()
    {
        // Arrange
        var userId = Guid.NewGuid();
        UserModel? expectedUser = _fixture.Build<UserModel>()
            .With(u => u.Id, userId)
            .Create();

        _userRepository.GetUserById(userId).Returns(Task.FromResult<UserModel?>(null));

        // Act
        var result = await _sut.GetUserById(userId);

        // Assert
        result.Should().BeNull();
    }

    //GetUserByName

    [Fact]
    public async Task GetUserByName_ShouldReturnUser_WhenNameIsValid()
    {
        //Arrange
        string username = "John";
        UserModel? expectedUser = _fixture.Build<UserModel>()
            .With(u => u.Username, username)
            .Create();

        _userRepository.GetUserByName(username)!.Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByName(username);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<UserModel>()
            .And.BeSameAs(expectedUser);
    }

    [Fact]
    public async Task GetUserByName_ShouldReturnNull_WhenUsernameIsInvalid()
    {
        //Arrange
        string username = "spiri";
        UserModel? expectedUser = null;

        _userRepository.GetUserByName(username).Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByName(username);

        //Assert
        result.Should().BeNull();
    }

    //GetUserByConfirmationCode

    [Fact]
    public async Task GetUserByConfirmationCode_ShouldReturnUser_WhenConfirmationCodeIsValid()
    {
        //Arrange
        Guid confirmationCode = Guid.NewGuid();
        UserModel? expectedUser = _fixture.Build<UserModel>()
            .With(u => u.ConfirmationCode, confirmationCode)
            .Create();

        _userRepository.GetUserByConfirmationCode(confirmationCode)!.Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByConfirmationCode(confirmationCode);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<UserModel>()
            .And.BeSameAs(expectedUser);
    }

    [Fact]
    public async Task GetUserByConfirmationCode_ShoudlReturnNull_WhenConfirmationCodeIsInvalid()
    {
        //Arrange
        Guid confirmationCode = Guid.NewGuid();
        UserModel? expectedUser = null;

        _userRepository.GetUserByConfirmationCode(confirmationCode).Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByConfirmationCode(confirmationCode);

        //Assert
        result.Should().BeNull();
    }

    //GetUserByRestorationCode
    [Fact]
    public async Task GetUserByRestorationCode_ShouldReturnUser_WhenRestorationCodeIsValid()
    {
        //Arrange
        Guid restorationCode = Guid.NewGuid();
        UserModel? expectedUser = _fixture.Build<UserModel>()
            .With(u => u.RestorationCode, restorationCode)
            .Create();

        _userRepository.GetUserByRestorationCode(restorationCode)!.Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByRestorationCode(restorationCode);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<UserModel>()
            .And.BeSameAs(expectedUser);
    }

    [Fact]
    public async Task GetUserByRestorationCode_ShouldReturnNull_WhenRestorationCodeIsInvalid()
    {
        //Arrange
        Guid restorationCode = Guid.NewGuid();
        UserModel? expectedUser = null;

        _userRepository.GetUserByRestorationCode(restorationCode).Returns(expectedUser);

        //Act
        var result = await _sut.GetUserByRestorationCode(restorationCode);

        //Assert
        result.Should().BeNull();
    }

    //UpdateUser
    [Fact]
    public async Task UpdateUser_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        UserModel? user = _fixture.Build<UserModel>()
            .With(u => u.Username, "John")
            .With(u => u.Bio, "This is my bio")
            .With(u => u.UpdatedDate, DateTime.UtcNow.AddHours(-3))
            .Create();

        _userRepository.UpdateUser(user).Returns(Task.CompletedTask);

        //Act
        await _sut.UpdateUser(user);

        //Assert
        await _userRepository.Received(1).UpdateUser(user);
        user.UpdatedDate.Should().BeCloseTo(DateTime.UtcNow.AddHours(-3), TimeSpan.FromSeconds(1));
    }

    //UpdateUserEmailConfirmation
    [Fact]
    public async Task UpdateUserEmail_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        UserModel? user = _fixture.Build<UserModel>()
            .With(u => u.Username, "John")
            .With(u => u.Email, "john@hotmail.com")
            .With(u => u.UpdatedDate, DateTime.UtcNow.AddHours(-3))
            .Create();

        _userRepository.UpdateUserEmailConfirmation(user).Returns(Task.CompletedTask);

        //Act
        await _sut.UpdateUserEmailConfirmation(user);

        //Assert
        await _userRepository.Received(1).UpdateUserEmailConfirmation(user);
        user.UpdatedDate.Should().BeCloseTo(DateTime.UtcNow.AddHours(-3), TimeSpan.FromSeconds(1));
    }

    //UpdateUserPassword
    [Fact]
    public async Task UpdateUserPassword_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        UserModel? user = _fixture.Build<UserModel>()
            .With(u => u.Username, "John")
            .With(u => u.Password, "Johnpassword123")
            .With(u => u.UpdatedDate, DateTime.UtcNow.AddHours(-3))
            .Create();

        _userRepository.UpdateUserPassword(user).Returns(Task.CompletedTask);

        //Act
        await _sut.UpdateUserPassword(user);

        //Assert
        await _userRepository.Received(1).UpdateUserPassword(user);
        user.UpdatedDate.Should().BeCloseTo(DateTime.UtcNow.AddHours(-3), TimeSpan.FromSeconds(1));
    }

    //DeleteUser
    [Fact]
    public async Task DeleteUser_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        Guid userId = Guid.NewGuid();

        _userRepository.DeleteUser(userId).Returns(Task.CompletedTask);

        //Act
        await _sut.DeleteUser(userId);

        //Assert
        await _userRepository.Received(1).DeleteUser(userId);
    }
}
