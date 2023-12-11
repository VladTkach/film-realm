using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using FilmRealm.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class FilmRepository : BaseRepository<Film>, IFilmRepository
{
    public FilmRepository(FilmRealmContext context) : base(context)
    {
    }

    public IQueryable<Film> GetFilms()
    {
        return _context.Films
            .Include(f => f.Genres)
            .Include(f => f.Actors)
            .AsQueryable();
    }

    public async Task<Film> GetFilmInternalByIdAsync(int id)
    {
        var film = await _context.Films
            .Include(f => f.Genres)
            .Include(f => f.Actors)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (film is null)
        {
            throw new NotFoundException(nameof(Film));
        }

        return film;
    }
}