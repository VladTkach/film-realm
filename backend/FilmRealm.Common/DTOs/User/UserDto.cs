namespace FilmRealm.Common.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public string? AvatarUrl { get; set; }
}