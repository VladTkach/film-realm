using Microsoft.AspNetCore.Http;

namespace FilmRealm.Common.DTOs.Film;

public class CreateFilmDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Year { get; set; }
    public IFormFile Poster { get; set; } = null!;
    public string ResourceUrl { get; set; } = null!;
    
    public List<int> GenresId { get; } = new();
    public List<int> ActorsId { get; } = new();
}