using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;
using FilmRealm.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.DAL.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
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

    public async Task<List<User>> GetAllAdminsAsync()
    {
        return await _context.Set<User>()
            .Include(u => u.UserRole)
            .Where(u => u.UserRole.Name == "Admin")
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByNameAsync(string userName)
    {
        return await _context.Set<User>()
            .Take(5)
            .Include(u => u.UserRole)
            .Where(u => u.UserRole.Name == "User")
            .Where(u => u.UserName.Contains(userName))
            .ToListAsync();

    }

    public async Task<User> GetUserInternalById(int id)
    {
        var user = await _context.Set<User>()
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            throw new NotFoundException(nameof(User));
        }

        return user;
    }
}