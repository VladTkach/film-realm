using FilmRealm.Common.DTOs.Genre;

namespace FilmRealm.BLL.Interfaces;

public interface IGenreService 
{
    Task<List<GenreDto>> GetAllGenresAsync();
    Task<GenreDto> AddGenreAsync(CreateGenreDto createGenreDto);
    Task<GenreDto> UpdateGenreAsync(UpdateGenreDto updateGenreDto);
    Task DeleteGenreAsync(int genreId);
}