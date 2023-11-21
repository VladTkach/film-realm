namespace FilmRealm.Shared.Exceptions;

public class UserNameAlreadyUsedException : Exception
{
    public UserNameAlreadyUsedException() : base("UserName already in use, try another")
    {
    }
}