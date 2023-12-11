using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all-admin")]
    public async Task<ActionResult<List<UserDto>>> GetAllAdminsAsync()
    {
        return Ok(await _userService.GetAllAdminsAsync());
    }
    
    [HttpGet("all-user/{userName}")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync(string userName)
    {
        return Ok(await _userService.GetUsersAsync(userName));
    }
    
    [HttpPost("add-admin/{id}")]
    public async Task<ActionResult<UserDto>> AddAdminAsync(int id)
    {
        return Ok(await _userService.PromoteUserAsync(id));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> DeleteAdminAsync(int id)
    {
        await _userService.DeleteAdminAsync(id);
        return NoContent();
    }
}