using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs;
using FilmRealm.Common.DTOs.Film;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpGet("all")]
    [AllowAnonymous]
    public ActionResult<PagedList<FilmDto>> GetAllFilmsAsync([FromQuery]SieveModel sieveModel)
    {
        return Ok(_filmService.GetPagedFilms(sieveModel));
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<PagedList<FilmDto>>> GetFilmByIdAsync(int id)
    {
        return Ok(await _filmService.GetFilmsByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<FilmDto>> AddFilmAsync([FromForm]CreateFilmDto createFilmDto)
    {
        return Ok(await _filmService.AddFilmAsync(createFilmDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFilmAsync(int id)
    {
        await _filmService.DeleteFilmAsync(id);
        return NoContent();
    }
}