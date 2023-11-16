using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class UserList : BaseEntity
{
    public int UserId { get; set; }
    public int FilmId { get; set; }
    public User User { get; set; } = null!;
    public Film Film { get; set; } = null!;
}