namespace FilmRealm.Common.DTOs.User;

public class UpdateUserPassword
{
    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}