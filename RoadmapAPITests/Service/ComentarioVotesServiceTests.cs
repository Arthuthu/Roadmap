using AutoFixture;
using NSubstitute;
using RoadmapRepository.Interfaces;
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
}
