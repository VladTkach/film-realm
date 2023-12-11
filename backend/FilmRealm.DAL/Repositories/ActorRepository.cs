using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class ActorRepository : BaseRepository<Actor>, IActorRepository
{
    public ActorRepository(FilmRealmContext context) : base(context)
    {
    }

    public Task<Actor?> GetActorByNameAsync(string name)
    {
        return _context.Actors.FirstOrDefaultAsync(a => a.Name == name);
    }
}