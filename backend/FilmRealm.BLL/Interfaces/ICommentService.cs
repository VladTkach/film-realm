using FilmRealm.Common.DTOs.Comment;

namespace FilmRealm.BLL.Interfaces;

public interface ICommentService
{

    Task<List<CommentDto>> GetFilmCommentsAsync(int filmId);
    Task<CommentDto> AddNewCommentAsync(CreateCommentDto createCommentDto);
    Task<CommentDto> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
    Task DeleteCommentAsync(int commentId);
}