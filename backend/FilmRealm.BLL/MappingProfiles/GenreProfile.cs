using AutoMapper;
using FilmRealm.Common.DTOs.Genre;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<CreateGenreDto, Genre>().ReverseMap();
        CreateMap<UpdateGenreDto, Genre>().ReverseMap();
    }
}