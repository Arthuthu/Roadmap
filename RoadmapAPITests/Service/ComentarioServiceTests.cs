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
			new ComentarioModel { Id = Guid.NewGuid(), Description = "Second Comment" }
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
		//Arrange
		Guid roadmapId = Guid.NewGuid();

		var expectedComentarios = Enumerable.Empty<ComentarioModel>();
		_comentarioRepository.GetAllComentarios(roadmapId).Returns(expectedComentarios);

		//Act
		var result = await _sut.GetAllComentarios(roadmapId);

		//Assert
		result.Should().NotBeNull().And.BeEmpty();
	}

	//GetComentarioById
	[Fact]
	public async Task GetComentarioById_ShoudlReturnComentario_WhenComentarioExists()
	{
		//Arrange
		Guid comentarioId = Guid.NewGuid();

		ComentarioModel? expectedComentario = _fixture.Build<ComentarioModel>()
			.With(x => x.Id, comentarioId)
			.With(x => x.Description, "Boa chefe")
			.Create();

		_comentarioRepository.GetComentarioById(comentarioId).Returns(expectedComentario);

		//Act
		var result = await _sut.GetComentarioById(comentarioId);

		//Assert
		result.Should().NotBeNull()
			.And.BeOfType<ComentarioModel>()
			.And.BeSameAs(expectedComentario);
	}

	[Fact]
	public async Task GetComentarioById_ShouldReturnNull_WhenThereAreNoComentariosWithGivenId()
	{
		//Arrange
		Guid comentarioId = Guid.NewGuid();
		ComentarioModel? comentario = null;

		_comentarioRepository.GetComentarioById(comentarioId).Returns(comentario);

		//Act
		var result = await _sut.GetComentarioById(comentarioId);

		//Assert
		result.Should().BeNull();
	}

	//CreateComentario
	[Fact]
	public async Task CreateComentario_ShouldCreateComentario_WhenComentarioIsValid()
	{
		//Arrange
		var comentario = _fixture.Build<ComentarioModel>()
			.With(x => x.Id, Guid.NewGuid())
			.With(x => x.Description, "Boa primo")
			.Create();

		_comentarioRepository.CreateComentario(Arg.Any<ComentarioModel>())
			.Returns(Task.CompletedTask);

		//Act
		await _sut.CreateComentario(comentario);

		//Assert
		await _comentarioRepository.Received(1)
			.CreateComentario(Arg.Is<ComentarioModel>(c => c.Id != Guid.Empty));
	}

	//UpdateComentario
	[Fact]
	public async Task UpdateComentario_ShouldCallUpdateComentarioMethodOfRepository_WhenValidComentarioIsProvided()
	{
		//Arrange
		var comentario = _fixture.Build<ComentarioModel>()
			.With(x => x.Id, Guid.NewGuid)
			.With(x => x.Description, "Boa cousin")
			.With(x => x.UpdatedDate, DateTime.UtcNow.AddHours(-3))
			.Create();

		//Act
		await _sut.UpdateComentario(comentario);

		//Assert
		await _comentarioRepository.Received(1).UpdateComentario(comentario);
		comentario.UpdatedDate.Should().BeCloseTo(DateTime.UtcNow.AddHours(-3), TimeSpan.FromSeconds(1));
	}

	//DeleteAllUserComentarios
	[Fact]
	public async Task DeleteAllUserComentarios_ShouldSendDataToRepository_WhenMethodIsCalled()
	{
		//Arrange
		Guid userId = Guid.NewGuid();

		_comentarioRepository.DeleteAllUserComentarios(userId).Returns(Task.CompletedTask);

		//Act
		await _comentarioRepository.DeleteAllUserComentarios(userId);

		//Assert
		await _comentarioRepository.Received(1).DeleteAllUserComentarios(userId);
	}

	//DeleteComentario
	[Fact]
	public async Task DeleteComentario_ShouldSendDataToRepository_WhenMethodIsCalled()
	{
		//Arrange
		Guid cometanrioId = Guid.NewGuid();

		_comentarioRepository.DeleteComentario(cometanrioId).Returns(Task.CompletedTask);

		//Act
		await _comentarioRepository.DeleteComentario(cometanrioId);

		//Assert
		await _comentarioRepository.Received(1).DeleteComentario(cometanrioId);
	}
}
