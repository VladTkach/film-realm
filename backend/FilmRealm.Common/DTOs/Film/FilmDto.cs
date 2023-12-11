using FilmRealm.Common.DTOs.Actor;
using FilmRealm.Common.DTOs.Genre;

namespace FilmRealm.Common.DTOs.Film;

public class FilmDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Year { get; set; }
    public string PosterUrl { get; set; } = null!;
    public string ResourceUrl { get; set; } = null!;
    
    public List<GenreDto> Genres { get; } = new();
    public List<ActorDto> Actors { get; } = new();
}