using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface IGenreRepository : IBaseRepository<Genre>
{
    Task<Genre?> GetGenreByNameAsync(string name);
}