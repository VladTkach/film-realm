using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities.Abstract;
using FilmRealm.DAL.Interfaces;
using FilmRealm.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories.Abstract;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly FilmRealmContext _context;

    protected BaseRepository(FilmRealmContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity is null)
        {
            throw new NotFoundException(nameof(entity));
        }

        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = (await _context.Set<TEntity>().AddAsync(entity)).Entity;
        await _context.SaveChangesAsync();
        return entityEntry;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }
}