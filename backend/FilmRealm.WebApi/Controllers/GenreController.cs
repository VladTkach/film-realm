using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.DTOs.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet("all")]
    [AllowAnonymous]
    public async Task<ActionResult<List<GenreDto>>> GetAllGenresAsync()
    {
        return Ok(await _genreService.GetAllGenresAsync());
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> CreateGenreAsync(CreateGenreDto createGenreDto)
    {
        return Ok(await _genreService.AddGenreAsync(createGenreDto));
    }

    [HttpPut]
    public async Task<ActionResult<GenreDto>> UpdateGenreAsync(UpdateGenreDto updateGenreDto)
    {
        return Ok(await _genreService.UpdateGenreAsync(updateGenreDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGenreAsync(int id)
    {
        await _genreService.DeleteGenreAsync(id);
        return NoContent();
    }
}