using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface IFilmRepository : IBaseRepository<Film>
{
    IQueryable<Film> GetFilms();

    Task<Film> GetFilmInternalByIdAsync(int id);
}