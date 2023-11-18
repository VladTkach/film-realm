namespace FilmRealm.Shared.Exceptions;

public class EmailAlreadyUsedException : Exception
{
    public EmailAlreadyUsedException() : base("This email already in using")
    {
    }
}