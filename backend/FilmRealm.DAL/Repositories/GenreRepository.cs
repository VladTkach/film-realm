using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(FilmRealmContext context) : base(context)
    {
    }
    
    public async Task<Genre?> GetGenreByNameAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
    }
}