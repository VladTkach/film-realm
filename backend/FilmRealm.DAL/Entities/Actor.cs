using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class Actor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Film> Films { get; set; } = new();
}