using AutoFixture;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;

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

	[Fact]
	public async Task GetAllComentarioVotes_ShouldReturnListOfComentarioVotes_WhenMethodIsCalled()
	{
		// Arrange
		Guid id = Guid.NewGuid();
		Guid userId = Guid.NewGuid();
		var expectedVotes = new List<ComentarioVotesModel>
	{
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
		new ComentarioVotesModel { Id = Guid.NewGuid(), ComentarioId = comentarioId, UserId = userId },
	};

		_comentarioVotesRepository.GetAllComentarioVotes().Returns(expectedVotes);

		// Act
		var result = await _sut.GetAllComentarioVotes();

		// Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(expectedVotes.Count);
		result.Should().BeEquivalentTo(expectedVotes);
	}
}
