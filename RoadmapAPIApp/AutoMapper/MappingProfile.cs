using AutoMapper;
using RoadmapAPIApp.Dtos;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserModel, UserDto>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapModel, RoadmapDto>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeModel, NodeDto>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();
	}
}
