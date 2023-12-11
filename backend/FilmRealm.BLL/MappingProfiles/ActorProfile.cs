using AutoMapper;
using FilmRealm.Common.DTOs.Actor;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class ActorProfile : Profile
{
    public ActorProfile()
    {
        CreateMap<Actor, ActorDto>().ReverseMap();
        CreateMap<CreateActorDto, Actor>().ReverseMap();
        CreateMap<UpdateActorDto, Actor>().ReverseMap();
    }
}