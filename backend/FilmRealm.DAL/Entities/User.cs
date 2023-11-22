using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public Guid? AvatarId { get; set; }
    public int UserRoleId { get; set; }
    public UserRole UserRole { get; set; } = null!;
    
    public List<Film> Films { get; set; } = new();
}