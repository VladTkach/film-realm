using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; } = string.Empty;
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public Film Film { get; set; } = null!;
    public User User { get; set; } = null!;
}