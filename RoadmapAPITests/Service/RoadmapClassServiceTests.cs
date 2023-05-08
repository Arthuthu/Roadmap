using FluentAssertions;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapAPITests.Service;

public class RoadmapClassServiceTests
{
	private readonly RoadmapClassService _sut;
	private readonly IRoadmapClassRepository _roadmapRepository = Substitute.For<IRoadmapClassRepository>();
	private readonly IMessageHandler _messageHandler = Substitute.For<IMessageHandler>();


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
}
