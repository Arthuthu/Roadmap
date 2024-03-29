﻿using AutoMapper;
using Roadmap.API.Request;
using Roadmap.Domain.Models;

namespace Roadmap.API.AutoMapper.Requests;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<UserModel, UserRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<ComentarioModel, ComentarioRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<UserModel, LoginRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<UserModel, RegisterRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<RoadmapClassModel, RoadmapClassRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<RoadmapVotesModel, RoadmapVotesRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<ComentarioVotesModel, ComentarioVotesRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<DenunciaModel, DenunciaRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();

        CreateMap<NodeModel, NodeRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();


    }
}
