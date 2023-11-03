using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Roadmap.Domain.Models;
using Roadmap.Domain.Repositories;
using Roadmap.Domain.SqlDataAccess;

namespace Roadmap.Tests.Repository;

public class ComentarioRepositoryTests
{
    private readonly ComentarioRepository _sut;
    private readonly ISqlDataAccess _db = Substitute.For<ISqlDataAccess>();
    private readonly IFixture _fixture = new Fixture();

    public ComentarioRepositoryTests()
    {
        _sut = new ComentarioRepository(_db);
    }

    //GetAllComentarios
    [Fact]
    public async Task GetAllComentarios_ShouldReturnListOfComentarios_WhenMethodIsCalled()
    {
        // Arrange
        Guid roadmapId = Guid.NewGuid();
        var expectedComentarios = new List<ComentarioModel>
    {
        new ComentarioModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, Description = "Comentario 1" },
        new ComentarioModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, Description = "Comentario 2" },
        new ComentarioModel { Id = Guid.NewGuid(), RoadmapId = roadmapId, Description = "Comentario 3" }
    };
        _db.LoadData<ComentarioModel, dynamic>("dbo.spComentario_GetAllComentariosByRoadmapId",
            Arg.Any<object>()).Returns(expectedComentarios);

        // Act
        var result = await _sut.GetAllComentarios(roadmapId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expectedComentarios.Count);
        result.Should().BeEquivalentTo(expectedComentarios);
    }

    [Fact]
    public async Task GetAllComentarios_ShouldReturnEmptyList_WhenNoComentariosFound()
    {
        // Arrange
        Guid roadmapId = Guid.NewGuid();
        var expectedComentarios = Enumerable.Empty<ComentarioModel>();

        _db.LoadData<ComentarioModel, dynamic>("dbo.spComentario_GetAllComentariosByRoadmapId",
            Arg.Any<object>()).Returns(expectedComentarios);

        // Act
        var result = await _sut.GetAllComentarios(roadmapId);

        // Assert
        result.Should().BeEmpty();
    }
}
