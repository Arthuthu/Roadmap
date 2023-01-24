using AutoMapper;
using RoadmapAPIApp.Request;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.AutoMapper.Requests;

public class RequestProfile : Profile
{
	public RequestProfile()
	{
		CreateMap<UserModel, UserRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapModel, RoadmapRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeModel, NodeRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();
	}
}
