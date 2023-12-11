namespace FilmRealm.Common.DTOs.Comment;

public class CreateCommentDto
{
    public string Text { get; set; } = null!;
    public int FilmId { get; set; }
    public int UserId { get; set; }
}