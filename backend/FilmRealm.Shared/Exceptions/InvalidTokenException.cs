namespace FilmRealm.Shared.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException() : base($"Invalid token.")
    {
    }
}