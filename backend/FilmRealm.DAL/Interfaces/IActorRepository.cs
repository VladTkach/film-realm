using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface IActorRepository : IBaseRepository<Actor>
{
    Task<Actor?> GetActorByNameAsync(string name);
}