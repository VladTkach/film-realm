using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    public UserRepository(FilmRealmContext context) : base(context)
    {
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return _context.Set<User>()
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetUserByNameAsync(string userName)
    {
        return _context.Set<User>()
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}