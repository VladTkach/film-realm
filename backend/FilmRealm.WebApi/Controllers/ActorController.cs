using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.Actor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;

    public ActorController(IActorService actorService)
    {
        _actorService = actorService;
    }

    [HttpGet("all")]
    [AllowAnonymous]
    public async Task<ActionResult<List<ActorDto>>> GetAllActorsAsync()
    {
        return Ok(await _actorService.GetAllActorsAsync());
    }

    [HttpPost]
    public async Task<ActionResult<ActorDto>> CreateGenreAsync(CreateActorDto createActorDto)
    {
        return Ok(await _actorService.AddActorAsync(createActorDto));
    }

    [HttpPut]
    public async Task<ActionResult<ActorDto>> UpdateGenreAsync(UpdateActorDto updateActorDto)
    {
        return Ok(await _actorService.UpdateActorAsync(updateActorDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGenreAsync(int id)
    {
        await _actorService.DeleteActorAsync(id);
        return NoContent();
    }
}