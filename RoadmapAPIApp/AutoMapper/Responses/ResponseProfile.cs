using AutoMapper;
using Domain.Models;
using RoadmapAPI.Response;

namespace RoadmapAPI.AutoMapper.Responses;

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

		CreateMap<ComentarioVotesResponse, ComentarioVotesModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<DenunciaResponse, DenunciaModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();

		CreateMap<NodeResponse, NodeModel>()
			.IgnoreAllPropertiesWithAnInaccessibleSetter()
			.ReverseMap();
	}
}
