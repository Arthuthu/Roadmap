using AutoFixture;
using FluentAssertions;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Classes;

namespace RoadmapAPITests.Service;

public class ComentarioServiceTests
{
	private readonly ComentarioService _sut;
	private readonly IComentarioRepository _comentarioRepository = Substitute.For<IComentarioRepository>();
	private readonly IFixture _fixture = new Fixture();

	public ComentarioServiceTests()
	{
		_sut = new ComentarioService(_comentarioRepository);
	}

	[Fact]
	public async Task GetAllComentarios_ShouldReturnAllComentarios_WhenThereIsComentarios()
	{
		//Arrange
		Guid roadmapId = Guid.NewGuid();

		var expectedComentarios = new List<ComentarioModel>()
		{
			new ComentarioModel { Id = Guid.NewGuid(), Description = "First Comment" },
			new ComentarioModel { Id = Guid.NewGuid(), Description = "Second Comment"}
		};

		_comentarioRepository.GetAllComentarios(roadmapId).Returns(expectedComentarios);

		//Act
		var result = await _sut.GetAllComentarios(roadmapId);

		//Assert
		result.Should().NotBeNull()
			.And.BeAssignableTo<IEnumerable<ComentarioModel>>()
			.And.HaveCount(2);
	}

	[Fact]
	public async Task GetAllComentarios_ShouldReturnEmptyList_WhenThereAreNoComentarios()
	{

	}
}
