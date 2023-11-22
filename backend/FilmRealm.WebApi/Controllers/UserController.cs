using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserService userService, IImageService imageService, IUserIdGetter userIdGetter) : ControllerBase
{
    [HttpGet("from-token")]
    public async Task<ActionResult<UserDto>> GetUserFromTokenAsync()
    {
        return Ok(await userService.GetUserById(userIdGetter.GetCurrentUserId()));
    }
    
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
    
    [HttpPost("add-avatar")]
    public async Task<ActionResult<UserDto>> AddUserAvatarAsync(IFormFile avatar)
    {
        return Ok(await imageService.AddAvatarAsync(avatar));
    }
    
    [HttpDelete("delete-avatar")]
    public async Task<ActionResult> DeleteUserAvatarAsync()
    {
        await imageService.DeleteAvatarAsync();
        return NoContent();
    }
}