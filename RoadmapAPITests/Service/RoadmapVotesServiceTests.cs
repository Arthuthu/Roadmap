using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Infra.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace RoadmapAPITests.Service;

public class RoadmapVotesServiceTests
{
	private readonly RoadmapVotesService _sut;
	private readonly IRoadmapVotesRepository _roadmapVotesRepository = Substitute.For<IRoadmapVotesRepository>();
	private readonly IFixture _fixture = new Fixture();

	public RoadmapVotesServiceTests()
	{
		_sut = new RoadmapVotesService(_roadmapVotesRepository);
	}

	//GetAllRoadmapVotes
	[Fact]
	public async Task GetAllRoadmapVotes_ShouldReturnListOfRoadmapVotes_WhenMethodIsCalled()
	{
		Guid roadmapId = Guid.NewGuid();
		Guid userId = Guid.NewGuid();
		var expectedVotes = new List<RoadmapVotesModel>
		{
			new RoadmapVotesModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, UserId = userId },
			new RoadmapVotesModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, UserId = userId },
			new RoadmapVotesModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, UserId = userId },
		};

		_roadmapVotesRepository.GetAllRoadmapVotes(userId, roadmapId).Returns(expectedVotes);

		// Act
		var result = await _sut.GetAllRoadmapVotes(userId, roadmapId);

		// Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(expectedVotes.Count);
		result.Should().BeEquivalentTo(expectedVotes);
	}

	[Fact]
	public async Task GetAllRoadmapVotes_ShouldReturnEmptyList_WhenThereAreNoVotes()
	{
		//Arrange
		Guid userId = Guid.NewGuid();
		Guid roadmapId = Guid.NewGuid();

		var expectedRoadmapVotes = Enumerable.Empty<RoadmapVotesModel>();

		_roadmapVotesRepository.GetAllRoadmapVotes(userId, roadmapId).Returns(expectedRoadmapVotes);

		//Act
		var result = await _sut.GetAllRoadmapVotes(userId, roadmapId);

		//Assert
		result.Should().NotBeNull().And.BeEmpty();
	}

	//GetAllRoadmapVotesByUserId
	[Fact]
	public async Task GetAllRoadmapVotesByUserId_ShouldReturnListOfRoadmapVotes_WhenMethodIsCalled()
	{
		//Arrange
		Guid userId = Guid.NewGuid();

		var expectedVotes = new List<RoadmapVotesModel>
		{
			new RoadmapVotesModel { Id = Guid.NewGuid(), UserId = userId },
			new RoadmapVotesModel { Id = Guid.NewGuid(), UserId = userId },
			new RoadmapVotesModel { Id = Guid.NewGuid(), UserId = userId },
		};

		_roadmapVotesRepository.GetAllRoadmapVotesByUserId(userId).Returns(expectedVotes);

		//Act
		var result = await _sut.GetAllRoadmapVotesByUserId(userId);

		//Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(expectedVotes.Count);
		result.Should().BeEquivalentTo(expectedVotes);
	}

	[Fact]
	public async Task GetAllRoadmapVotesByUserId_ShouldReturnEmptyList_WhenThereAreNoVotes()
	{
		//Arrange
		Guid userId = Guid.NewGuid();

		var expectedVotes = Enumerable.Empty<RoadmapVotesModel>();

		_roadmapVotesRepository.GetAllRoadmapVotesByUserId(userId).Returns(expectedVotes);

		//Act
		var result = await _sut.GetAllRoadmapVotesByUserId(userId);

		//Assert
		result.Should().NotBeNull().And.BeEmpty();
	}

	//AdRoadmapVote
	[Fact]
	public async Task AddRoadmapVote_ShouldReturnSuccessMessage_WhenVoteIsAdded()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Guid roadmapId = Guid.NewGuid();
		_roadmapVotesRepository.AddRoadmapVote(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<Guid>())
			.Returns(Task.CompletedTask);

		// Act
		var result = await _sut.AddRoadmapVote(userId, roadmapId);

		// Assert
		result.Should().NotBeNull();
		result.Should().Be("Voto adicionado com sucesso");
	}

	[Fact]
	public async Task AddRoadmapVote_ShouldReturnErrorMessage_WhenRepositoryThrowsException()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Guid roadmapId = Guid.NewGuid();
		_roadmapVotesRepository
			.When(x => x.AddRoadmapVote(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<Guid>()))
			.Throw(new Exception("Repository exception"));

		// Act
		string result = await _sut.AddRoadmapVote(userId, roadmapId);

		// Assert
		result.Should().NotBeNullOrEmpty();
		result.Should().Be("Ocorreu um erro ao adicionar o voto");
	}

	//DeleteRoadmapVote
	[Fact]
	public async Task DeleteRoadmapVote_ShouldDeleteRoadmapVote_WhenValidIdIsProvided()
	{
		// Arrange
		Guid id = Guid.NewGuid();

		// Act
		await _sut.DeleteRoadmapVote(id);

		// Assert
		await _roadmapVotesRepository.Received(1).DeleteRoadmapVote(id);
	}

	[Fact]
	public async Task DeleteRoadmapVote_ShouldThrowException_WhenRepositoryThrowsException()
	{
		// Arrange
		Guid comentarioVoteId = Guid.NewGuid();
		_roadmapVotesRepository.DeleteRoadmapVote(comentarioVoteId).Throws(new Exception("Database connection failed"));

		// Act and Assert
		await Assert.ThrowsAsync<Exception>(async () => await _sut.DeleteRoadmapVote(comentarioVoteId));
	}
}
