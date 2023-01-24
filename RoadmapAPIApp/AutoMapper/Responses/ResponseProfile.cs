using AutoMapper;
using RoadmapAPIApp.Request;
using RoadmapAPIApp.Response;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.AutoMapper.Responses;

public class ResponseProfile : Profile
{
	public ResponseProfile()
	{
		CreateMap<UserResponse, UserModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapClassResponse, RoadmapClassModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeResponse, NodeModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();
	}
}
