using FilmRealm.DAL.Entities;

namespace FilmRealm.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByNameAsync(string userName);

    Task<List<User>> GetAllAdminsAsync();
    Task<List<User>> GetUsersByNameAsync(string userName);
    Task<User> GetUserInternalById(int id);
}