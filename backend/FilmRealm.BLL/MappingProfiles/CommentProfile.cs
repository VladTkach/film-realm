using AutoMapper;
using FilmRealm.Common.DTOs.Comment;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        CreateMap<Comment, UpdateCommentDto>().ReverseMap();
    }
}