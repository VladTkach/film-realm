using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Film> Films { get; set; } = new();
}