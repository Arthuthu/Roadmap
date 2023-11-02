using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Infra.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace RoadmapAPITests.Service;

public class ComentarioVotesServiceTests
{
	private readonly ComentarioVotesService _sut;
	private readonly IComentarioVotesRepository _comentarioVotesRepository = Substitute.For<IComentarioVotesRepository>();
	private readonly IFixture _fixture = new Fixture();

	public ComentarioVotesServiceTests()
	{
		_sut = new ComentarioVotesService(_comentarioVotesRepository);
	}

	//GetAllComentarioVotes
	[Fact]
	public async Task GetAllComentarioVotes_ShouldReturnListOfComentarioVotes_WhenMethodIsCalled()
	{
		// Arrange
		Guid comentarioId = Guid.NewGuid();
		Guid userId = Guid.NewGuid();
		var expectedVotes = new List<ComentarioVotesModel>
	{
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
	};

		_comentarioVotesRepository.GetAllComentarioVotes(userId, comentarioId).Returns(expectedVotes);

		// Act
		var result = await _sut.GetAllComentarioVotes(userId, comentarioId);

		// Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(expectedVotes.Count);
		result.Should().BeEquivalentTo(expectedVotes);
	}

	[Fact]
	public async Task GetAllComentarioVotes_ShouldReturnEmptyList_WhenThereAreNoVotes()
	{
		//Arrange
		Guid userId = Guid.NewGuid();
		Guid comentarioId = Guid.NewGuid();

		var expectedComentarioVotes = Enumerable.Empty<ComentarioVotesModel>();

		_comentarioVotesRepository.GetAllComentarioVotes(userId, comentarioId).Returns(expectedComentarioVotes);

		//Act
		var result = await _sut.GetAllComentarioVotes(userId, comentarioId);

		//Assert
		result.Should().NotBeNull().And.BeEmpty();
	}

	//AddComentarioVote
	[Fact]
	public async Task AddComentarioVote_ShouldReturnSuccessMessage_WhenVoteIsAdded()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Guid comentarioId = Guid.NewGuid();
		_comentarioVotesRepository.AddComentarioVote(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<Guid>())
			.Returns(Task.CompletedTask);

		// Act
		var result = await _sut.AddComentarioVote(userId, comentarioId);

		// Assert
		result.Should().NotBeNull();
		result.Should().Be("Voto adicionado com sucesso");
	}

	[Fact]
	public async Task AddComentarioVote_ShouldReturnErrorMessage_WhenRepositoryThrowsException()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Guid comentarioId = Guid.NewGuid();
		_comentarioVotesRepository
			.When(x => x.AddComentarioVote(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<Guid>()))
			.Throw(new Exception("Repository exception"));

		// Act
		string result = await _sut.AddComentarioVote(userId, comentarioId);

		// Assert
		result.Should().NotBeNullOrEmpty();
		result.Should().Be("Ocorreu um erro ao adicionar o voto");
	}

	//DeleteComentarioVote
	[Fact]
	public async Task DeleteComentarioVote_ShouldDeleteComentarioVote_WhenValidIdIsProvided()
	{
		// Arrange
		Guid id = Guid.NewGuid();

		// Act
		await _sut.DeleteComentarioVote(id);

		// Assert
		await _comentarioVotesRepository.Received(1).DeleteComentarioVote(id);
	}

	[Fact]
	public async Task DeleteComentarioVote_ShouldThrowException_WhenRepositoryThrowsException()
	{
		// Arrange
		Guid comentarioVoteId = Guid.NewGuid();
		_comentarioVotesRepository.DeleteComentarioVote(comentarioVoteId).Throws(new Exception("Database connection failed"));

		// Act and Assert
		await Assert.ThrowsAsync<Exception>(async () => await _sut.DeleteComentarioVote(comentarioVoteId));
	}
}
