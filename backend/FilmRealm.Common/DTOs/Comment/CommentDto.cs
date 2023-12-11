using FilmRealm.Common.DTOs.User;

namespace FilmRealm.Common.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}