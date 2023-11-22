using AutoMapper;
using FilmRealm.BLL.MappingProfiles.MappingActions;
using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().AfterMap<BuildAvatarLinkAction>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
    }
}