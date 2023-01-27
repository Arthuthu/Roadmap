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

		CreateMap<UserModel, LoginRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapClassModel, RoadmapClassRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeModel, NodeRequest>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();


	}
}
