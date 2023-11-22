namespace FilmRealm.Shared.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password, try again")
    {
    }
}