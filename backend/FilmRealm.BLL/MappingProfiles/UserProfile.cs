using AutoMapper;
using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
    }
}