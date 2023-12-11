using FilmRealm.DAL.Entities.Abstract;

namespace FilmRealm.DAL.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(int id);

    Task<TEntity> AddAsync(TEntity entity);

    void Delete(TEntity entity);

    Task DeleteByIdAsync(int id);

    void Update(TEntity entity);
}