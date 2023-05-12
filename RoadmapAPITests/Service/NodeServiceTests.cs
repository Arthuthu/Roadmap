using AutoFixture;
using FluentAssertions;
using NodeServices.Classes;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RoadmapRepository.Classes;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;

namespace RoadmapAPITests.Service;

public class NodeServiceTests
{
	private readonly NodeService _sut;
	private readonly INodeRepository _nodeRepository = Substitute.For<INodeRepository>();
	private readonly IFixture _fixture = new Fixture();

    public NodeServiceTests()
    {
        _sut = new NodeService(_nodeRepository);
    }

	//GetAllNodes
	[Fact]
	public async Task GetAllNodes_ShouldReturnNodes_WhenThereNodesInTheRoadmap()
	{
		//Arrange
		Guid roadmapId = Guid.NewGuid();

		var expectedNodes = new List<NodeModel>
		{
			new NodeModel { Id = Guid.NewGuid(), Description = ".NET", RoadmapId = roadmapId },
			new NodeModel { Id = Guid.NewGuid(), Description = "SQL Server", RoadmapId = roadmapId },
			new NodeModel { Id = Guid.NewGuid(), Description = "AWS", RoadmapId = roadmapId }
		};

		_nodeRepository.GetAllNodes(roadmapId).Returns(expectedNodes);

		//Act
		var result = await _sut.GetAllNodes(roadmapId);

		//Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(expectedNodes.Count());
		result.Should().BeEquivalentTo(expectedNodes);
	}

	[Fact]
	public async Task GetAllNodes_ShouldReturnEmptyList_WhenThereAreNoNodesInTheRoadmap()
	{
		//Arrange
		Guid roadmapId = Guid.NewGuid();
		var expectedNodes = Enumerable.Empty<NodeModel>();

		_nodeRepository.GetAllNodes(roadmapId).Returns(expectedNodes);

		//Act
		var result = await _sut.GetAllNodes(roadmapId);

		//Assert
		result.Should().NotBeNull()
			.And.BeEmpty();
	}

	//GetNodeById
	[Fact]
	public async Task GetNodeById_ShouldReturnNode_WhenIdIsValid()
	{
		//Arrange
		Guid nodeId = Guid.NewGuid();
		NodeModel node = _fixture.Build<NodeModel>()
			.With(x => x.Id, nodeId)
			.With(x => x.Description, "C#")
			.With(x => x.CreatedDate, DateTime.UtcNow.AddHours(-3))
			.Create();

		_nodeRepository.GetNodeById(nodeId).Returns(node);

		//Act
		var result = await _sut.GetNodeById(nodeId);

		//Assert
		result.Should().NotBeNull()
			.And.BeSameAs(node);
	}

	[Fact]
	public async Task GetNodeById_ShouldReturnNull_WhenThereAreNoNodeWithGivenId()
	{
		//Arrange
		Guid nodeId = Guid.NewGuid();
		NodeModel? expectedNode = null;

		_nodeRepository.GetNodeById(nodeId).Returns(expectedNode);

		//Act
		var result = await _nodeRepository.GetNodeById(nodeId);

		//Assert

		result.Should().BeNull();
	}

	//AddNode
	[Fact]
	public async Task AddNode_ShouldAddTheNode_WhenNodeIsValid()
	{
		//Arrange
		NodeModel node = _fixture.Build<NodeModel>()
			.With(x => x.Id, Guid.NewGuid())
			.With(x => x.Description, "C#")
			.With(x => x.CreatedDate, DateTime.UtcNow.AddHours(-3))
			.Create();

		//Act
		await _sut.AddNode(node);

		//Assert
		await _nodeRepository.Received(1)
			.AddNode(Arg.Is<NodeModel>(x => x.Id != Guid.Empty));
	}

	[Fact]
	public async Task AddNode_ShouldThrowException_WhenAddingNodeToDatabaseFails()
	{
		//Arrange
		NodeModel node = _fixture.Build<NodeModel>()
			.With(x => x.Id, Guid.NewGuid())
			.With(x => x.Description, "C#")
			.With(x => x.CreatedDate, DateTime.UtcNow.AddHours(-3))
			.Create();
		var exceptionMessage = "Failed to add node";

		_nodeRepository.AddNode(Arg.Any<NodeModel>())
			.ThrowsAsyncForAnyArgs(new Exception("Failed to add node"));

		//Act
		var action = async () => await _sut.AddNode(node);

		//Assert
		var exception = await Assert.ThrowsAsync<Exception>(action);
		exception.Message.Should().Be($"Ocorreu um erro ao adicionado o node {exceptionMessage}");
	}

	//UpdateNode
	[Fact]
	public async Task UpdatedNode_ShouldSendDataToRepository_WhenMethodIsCalled()
	{
		//Arrange
		NodeModel node = _fixture.Build<NodeModel>()
			.With(x => x.Id, Guid.NewGuid())
			.With(x => x.Description, "C#")
			.With(x => x.UpdatedDate, DateTime.UtcNow.AddHours(-3))
			.Create();

		_nodeRepository.UpdateNode(node).Returns(Task.CompletedTask);

		//Act
		await _sut.UpdateNode(node);

		//Assert
		await _nodeRepository.Received(1).UpdateNode(node);
	}

	//DeleteNode
	[Fact]
	public async Task DeleteNode_ShouldSendDataToRepository_WhenMethodIsCalled()
	{
		//Arrange
		Guid nodeId = Guid.NewGuid();

		_nodeRepository.DeleteNode(nodeId).Returns(Task.CompletedTask);

		//Act
		await _sut.DeleteNode(nodeId);

		//Assert
		await _nodeRepository.Received(1).DeleteNode(nodeId);
	}
}
