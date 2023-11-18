using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}