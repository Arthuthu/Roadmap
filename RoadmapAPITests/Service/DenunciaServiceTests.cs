using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Roadmap.Domain.Interfaces;
using Roadmap.Domain.Models;
using Roadmap.Infra.Services;

namespace Roadmap.Tests.Service;

public class DenunciaServiceTests
{
    private readonly DenunciaService _sut;
    private readonly IDenunciaRepository _denunciaRepository = Substitute.For<IDenunciaRepository>();
    private readonly IFixture _fixture = new Fixture();

    public DenunciaServiceTests()
    {
        _sut = new DenunciaService(_denunciaRepository);
    }

    //GetAllDenuncias
    [Fact]
    public async Task GetAllDenuncias_ShouldReturnDenuncias_WhenThereAreDenuncias()
    {
        //Arrange
        var expectedDenuncias = new List<DenunciaModel>
        {
            new DenunciaModel { Id = Guid.NewGuid(), Description = "First Denuncia", AuthorId = Guid.NewGuid() },
            new DenunciaModel { Id = Guid.NewGuid(), Description = "Second Denuncia", AuthorId = Guid.NewGuid() },
            new DenunciaModel { Id = Guid.NewGuid(), Description = "Third Denuncia", AuthorId= Guid.NewGuid() }
        };

        _denunciaRepository.GetAllDenuncias().Returns(expectedDenuncias);

        //Act
        var result = await _sut.GetAllDenuncias();

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expectedDenuncias.Count());
        result.Should().BeEquivalentTo(expectedDenuncias);
    }

    [Fact]
    public async Task GetAllDenuncias_ShouldReturnEmptyList_WhenThereAreNoDenuncias()
    {
        //Arrange
        var expectedDenuncias = Enumerable.Empty<DenunciaModel>();

        _denunciaRepository.GetAllDenuncias().Returns(expectedDenuncias);

        //Act
        var result = await _sut.GetAllDenuncias();

        //Assert
        result.Should().NotBeNull()
            .And.BeEmpty();
    }

    //GetDenunciaById
    [Fact]
    public async Task GetDenunciaById_ShouldReturnDenuncia_WhenIdDenunciaIsFound()
    {
        //Arrange
        Guid denunciaId = Guid.NewGuid();
        DenunciaModel denuncia = _fixture.Build<DenunciaModel>()
            .With(x => x.Id, denunciaId)
            .With(x => x.Description, "Denuncia description")
            .Create();

        _denunciaRepository.GetDenunciaById(denunciaId).Returns(denuncia);

        //Act
        var result = await _sut.GetDenunciaById(denunciaId);

        //Assert
        result.Should().NotBeNull()
            .And.BeOfType<DenunciaModel>()
            .And.BeSameAs(denuncia);
    }

    [Fact]
    public async Task GetDenunciaById_ShouldReturnNull_WhenThereAreNoDenunciasWithGivenId()
    {
        //Arrange
        Guid denunciaId = Guid.NewGuid();
        DenunciaModel? denuncia = null;

        _denunciaRepository.GetDenunciaById(denunciaId).Returns(denuncia);

        //Act
        var result = await _sut.GetDenunciaById(denunciaId);

        //Assert
        result.Should().BeNull();
    }

    //CreateDenuncia
    [Fact]
    public async Task CreateDenuncia_ShouldCreateDenuncia_WhenMethodIsCalled()
    {
        //Arrange
        DenunciaModel denuncia = _fixture.Build<DenunciaModel>()
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Description, "Denuncia description")
            .With(x => x.CreatedDate, DateTime.UtcNow.AddHours(-3))
            .Create();

        //Act
        await _sut.CreateDenuncia(denuncia);

        //Assert
        await _denunciaRepository.Received(1)
            .CreateDenuncia(Arg.Is<DenunciaModel>(x => x.Id != Guid.Empty));
    }

    //UpdateDenuncia
    [Fact]
    public async Task UpdateDenuncia_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        DenunciaModel denuncia = _fixture.Build<DenunciaModel>()
            .With(x => x.Id, Guid.NewGuid())
            .With(x => x.Description, "Updated Description")
            .With(x => x.UpdatedDate, DateTime.UtcNow.AddHours(-3))
            .Create();

        //Act
        await _denunciaRepository.UpdateDenuncia(denuncia);

        //Assert
        await _denunciaRepository.Received(1).UpdateDenuncia(denuncia);
        denuncia.UpdatedDate.Should().BeCloseTo(DateTime.UtcNow.AddHours(-3), TimeSpan.FromSeconds(1));
    }

    //DeleteDenuncia
    [Fact]
    public async Task DeleteDenuncia_ShouldSendDataToRepository_WhenMethodIsCalled()
    {
        //Arrange
        Guid denunciaId = Guid.NewGuid();

        _denunciaRepository.DeleteDenuncia(denunciaId).Returns(Task.CompletedTask);

        //Act
        await _denunciaRepository.DeleteDenuncia(denunciaId);

        //Assert
        await _denunciaRepository.Received(1).DeleteDenuncia(denunciaId);
    }
}
