using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDto>> LoginAsync([FromBody] UserLoginDto userLoginData)
    {
        return Ok(await _authService.AuthenticateUserAsync(userLoginData));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthUserDto>> RegisterAsync([FromBody] CreateUserDto createUserDto)
    {
        return Ok(await _authService.RegisterAsync(createUserDto));
    }
}