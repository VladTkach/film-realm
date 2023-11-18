using FilmRealm.Common.DTOs.Auth;
using FilmRealm.Common.JWT;

namespace FilmRealm.Common.DTOs.User;

public class AuthUserDto
{
    public UserDto User { get; set; } = null!;
    
    public RefreshedAccessTokenDto Token { get; set; } = null!;
}