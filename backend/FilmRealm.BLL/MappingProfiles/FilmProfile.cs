using AutoMapper;
using FilmRealm.BLL.MappingProfiles.MappingActions;
using FilmRealm.Common.DTOs.Film;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<Film, FilmDto>().AfterMap<BuildAvatarLinkAction>().ReverseMap();
        CreateMap<CreateFilmDto, Film>().ReverseMap();
    }
}