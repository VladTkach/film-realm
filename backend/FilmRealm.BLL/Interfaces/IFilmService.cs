using FilmRealm.Common.DTOs;
using FilmRealm.Common.DTOs.Film;
using Sieve.Models;

namespace FilmRealm.BLL.Interfaces;

public interface IFilmService
{
    Task<FilmDto> GetFilmsByIdAsync(int filmId);
    PagedList<FilmDto> GetPagedFilms(SieveModel sieveModel);
    Task<FilmDto> AddFilmAsync(CreateFilmDto createFilmDto);
    // Task<FilmDto> UpdateGenreAsync(UpdateGenreDto updateGenreDto);
    Task DeleteFilmAsync(int filmId);
}