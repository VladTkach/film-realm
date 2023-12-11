using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs.Comment;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;

namespace FilmRealm.BLL.Services;

public class CommentService(IMapper mapper, ICommentRepository commentRepository, IFilmRepository filmRepository,
    IUserRepository userRepository) : BaseService(mapper), ICommentService
{
    public async  Task<List<CommentDto>> GetFilmCommentsAsync(int filmId)
    {
        return _mapper.Map<List<CommentDto>>(await commentRepository.GetFilmComments(filmId));
    }

    public async Task<CommentDto> AddNewCommentAsync(CreateCommentDto createCommentDto)
    {
        await userRepository.GetByIdAsync(createCommentDto.UserId);
        await filmRepository.GetByIdAsync(createCommentDto.FilmId);
        
        var comment = await commentRepository.AddAsync(_mapper.Map<Comment>(createCommentDto));
        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<CommentDto> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
    {
        var comment = await commentRepository.GetByIdAsync(updateCommentDto.Id);
        comment.Text = updateCommentDto.Text;
        
        commentRepository.Update(comment);
        return _mapper.Map<CommentDto>(comment);
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        await commentRepository.DeleteByIdAsync(commentId);
    }
}