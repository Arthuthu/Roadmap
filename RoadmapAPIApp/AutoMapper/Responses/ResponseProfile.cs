using AutoMapper;
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

		CreateMap<ComentarioResponse, ComentarioModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapClassResponse, RoadmapClassModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<RoadmapVotesResponse, RoadmapVotesModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeResponse, NodeModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();
	}
}
