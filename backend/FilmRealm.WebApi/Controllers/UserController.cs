using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPut("update-username")]
    public async Task<ActionResult<UserDto>> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto)
    {
        return Ok(await userService.UpdateUserNameAsync(updateUserNameDto));
    }
    
    [HttpPut("update-password")]
    public async Task<ActionResult<UserDto>> UpdateUserPasswordAsync(UpdateUserPassword updateUserPassword)
    {
        return Ok(await userService.UpdateUserPassword(updateUserPassword));
    }
}