using FilmRealm.DAL.Entities;
using Sieve.Services;

namespace FilmRealm.BLL.Sieve;

public class SieveCustomFilterMethods : ISieveCustomFilterMethods
{
    public IQueryable<Film> FilmIsAnyOf(IQueryable<Film> source, string op, string[] value)
    {
        var result = source.Where(film =>
            value.All(genre =>
                film.Genres.Any(filmGenre => filmGenre.Name == genre)
            )
        );
    
        return result;
    }
}