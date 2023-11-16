using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories.Abstract;

namespace FilmRealm.DAL.Repositories;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    public UserRepository(FilmRealmContext context) : base(context)
    {
    }
}