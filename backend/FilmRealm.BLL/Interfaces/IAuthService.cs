using FilmRealm.Common.DTOs.User;

namespace FilmRealm.BLL.Interfaces;

public interface IAuthService
{
    Task<AuthUserDto> AuthenticateUserAsync(UserLoginDto userLoginDto);
    public Task<AuthUserDto> RegisterAsync(CreateUserDto createUserDto);
}