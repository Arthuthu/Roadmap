using AutoFixture;
using NSubstitute;
using RoadmapRepository.Interfaces;
using RoadmapServices.Classes;

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
}
