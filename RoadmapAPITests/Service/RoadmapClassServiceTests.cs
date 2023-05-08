using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;
using RoadmapServices.Validators;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapAPITests.Service;

public class RoadmapClassServiceTests
{
	private readonly RoadmapClassService _sut;
	private readonly IRoadmapClassRepository _roadmapRepository = Substitute.For<IRoadmapClassRepository>();
	private readonly IMessageHandler _messageHandler = Substitute.For<IMessageHandler>();
	private readonly IFixture _fixture = new Fixture();

	public RoadmapClassServiceTests()
    {
        _sut = new RoadmapClassService(_roadmapRepository, _messageHandler);
    }

    //GetAllRoadmaps
    [Fact]
    public async Task GetAllRoadmaps_ShouldReturnAllRoadmaps_WhenThereIsRoadmaps()
    {
        //Arrange
        var expectedRoadmaps = new List<RoadmapClassModel>()
        {
            new RoadmapClassModel { Id = Guid.NewGuid(), Name = "C# Roadmap", Description = "Description"},
            new RoadmapClassModel { Id = Guid.NewGuid(), Name = "Javascript Roadmap", Description = "Description"},
            new RoadmapClassModel { Id = Guid.NewGuid(), Name = "C++ Roadmap", Description= "Description"}
        };

        _roadmapRepository.GetAllRoadmaps().Returns(expectedRoadmaps);

        //Act
        var result = await _sut.GetAllRoadmaps();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedRoadmaps);
    }

    [Fact]
    public async Task GetAllRoadmaps_ShouldReturnEmptyList_WhenThereAreNoRoadmaps()
    {
        //Arrange
        var expectedRoadmaps = Enumerable.Empty<RoadmapClassModel>();
        _roadmapRepository.GetAllRoadmaps().Returns(expectedRoadmaps);

        //Act
        var result = await _sut.GetAllRoadmaps();

        //Assert
        result.Should().NotBeNull().And.BeEmpty();
    }

    //GetRoadmapById
    [Fact]
    public async Task GetRoadmapById_ShouldReturnRoadmap_WhenIdIsValid()
    {
        //Arrange
        var roadmapId = Guid.NewGuid();

        RoadmapClassModel expectedRoadmap = _fixture.Build<RoadmapClassModel>()
            .With(x => x.Id, roadmapId)
            .With(x => x.Name, "C# Roadmap")
            .Create();

        _roadmapRepository.GetRoadmapById(roadmapId).Returns(expectedRoadmap);

        //Act
        var result = await _sut.GetRoadmapById(roadmapId);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<RoadmapClassModel>()
            .And.BeSameAs(expectedRoadmap);
    }

    [Fact]
    public async Task GetRoadmapById_ShouldReturnNull_WhenThereAreNoRoadmaps()
    {
        //Arrange
        Guid roadmapId = Guid.NewGuid();
        RoadmapClassModel? expectedRoadmap = null;

        _roadmapRepository.GetRoadmapById(roadmapId).Returns(expectedRoadmap);

        //Act
        var result = await _sut.GetRoadmapById(roadmapId);

        //Assert
        result.Should().BeNull();
    }

    //GetRoadmapsByUserId
    [Fact]
	public async Task GetRoadmapByUserId_ShouldReturnRoadmap_WhenUserIdIsValid()
	{
        //Arrange
        var userId = Guid.NewGuid();

		var expectedRoadmaps = new List<RoadmapClassModel>()
		{
			new RoadmapClassModel { Id = Guid.NewGuid(), Name = "C# Roadmap", UserId = userId},
			new RoadmapClassModel { Id = Guid.NewGuid(), Name = "Javascript Roadmap", UserId = userId},
			new RoadmapClassModel { Id = Guid.NewGuid(), Name = "C++ Roadmap", UserId = userId}
		};


		_roadmapRepository.GetRoadmapByUserId(userId).Returns(expectedRoadmaps);

		//Act
		var result = await _sut.GetRoadmapsByUserId(userId);

		//Assert
		result.Should().NotBeNull()
		    .And.BeSameAs(expectedRoadmaps);
	}

    [Fact]
	public async Task GetRoadmapByUserId_ShouldReturnEmptyList_WhenUserIdIsValid()
	{
		//Arrange
		var userId = Guid.NewGuid();

		_roadmapRepository.GetRoadmapByUserId(userId).Returns((IList<RoadmapClassModel>)null!);

        //Act
        Func<Task> action = async () => await _sut.GetRoadmapsByUserId(userId);

        //Assert
        await action.Should().ThrowAsync<Exception>()
            .WithMessage("Usuario não tem roadmaps criados");
	}

    //AddRoadmap
    [Fact]
    public async Task AddRoadmap_ShouldCreateRoadmap_WhenRoadmapDataIsValid()
    {
        //Arrange
        var roadmap = _fixture.Build<RoadmapClassModel>()
            .With(x => x.Name, "C# Roadmap")
            .With(x => x.Description, "Roadmap Description")
            .With(x => x.Category, "C#")
            .Create();

        var roadmapValidator = new RoadmapValidator();
        var validationResults = roadmapValidator.TestValidate(roadmap);

		_roadmapRepository.AddRoadmap(Arg.Any<RoadmapClassModel>()).Returns(Task.CompletedTask);

		//Act
		var result = await _sut.AddRoadmap(roadmap);
		Console.WriteLine($"Registration messages: {string.Join(", ", result)}");

		//Assert
		validationResults.ShouldNotHaveAnyValidationErrors();

        result.Should().NotBeNull()
            .And.HaveCount(1)
            .And.Contain("Roadmap criado com sucesso");

		await _roadmapRepository.Received(1).AddRoadmap(Arg.Is<RoadmapClassModel>(x => x.Id == roadmap.Id));
	}
}
