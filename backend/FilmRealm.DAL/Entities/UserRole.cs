using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Entities;

public class UserRole: BaseEntity
{
    public string Name { get; set; } = string.Empty;
}