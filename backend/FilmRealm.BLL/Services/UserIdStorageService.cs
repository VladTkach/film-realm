using FilmRealm.BLL.Interfaces;
using FilmRealm.Shared.Exceptions;

namespace FilmRealm.BLL.Services;

public class UserIdStorageService : IUserIdGetter, IUserIdSetter
{
    private int _userId;

    public int GetCurrentUserId()
    {
        if (_userId == 0)
        {
            throw new InvalidTokenException();
        }

        return _userId;
    }

    public void SetCurrentUserId(int id)
    {
        _userId = id;
    }
}