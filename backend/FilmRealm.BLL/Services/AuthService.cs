using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs.Auth;
using FilmRealm.Common.DTOs.User;
using FilmRealm.Common.Interfaces;
using FilmRealm.Common.Security;
using FilmRealm.Shared.Exceptions;

namespace FilmRealm.BLL.Services;

public class AuthService(IMapper mapper, IJwtFactory jwtFactory, IUserService userService) : BaseService(mapper),
    IAuthService
{
    public async Task<AuthUserDto> AuthenticateUser(UserLoginDto userLoginDto)
    {
        var user = await userService.GetUserByEmailAsync(userLoginDto.Email);

        if (user is null ||
            !SecurityHelper.PasswordValidation(userLoginDto.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            throw new InvalidEmailOrPasswordException();
        }

        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(user),
            Token = await GenerateNewAccessTokenAsync(user.Id, user.UserName, user.Email, user.UserRole.Name)
        };
    }

    public async Task<AuthUserDto> RegisterAsync(CreateUserDto createUserDto)
    {
        var createdUser = await userService.CreateUserAsync(createUserDto);

        return new AuthUserDto
        {
            User = _mapper.Map<UserDto>(createdUser),
            Token = await GenerateNewAccessTokenAsync(createdUser.Id, createdUser.UserName, createdUser.Email,
                createdUser.UserRole.Name)
        };
    }

    private async Task<RefreshedAccessTokenDto> GenerateNewAccessTokenAsync(int userId, string userName, string email,
        string role)
    {
        var refreshToken = jwtFactory.GenerateRefreshToken();

        // await _context.RefreshTokens.AddAsync(new RefreshToken
        // {
        //     Token = refreshToken,
        //     UserId = userId
        // });
        // await _context.SaveChangesAsync();

        var accessToken = await jwtFactory.GenerateAccessToken(userId, userName, email, role);

        return new RefreshedAccessTokenDto(accessToken, refreshToken);
    }
}