using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities.Abstract;
using FilmRealm.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories.Abstract;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly FilmRealmContext _context;

    protected BaseRepository(FilmRealmContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }
}