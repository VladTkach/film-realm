namespace FilmRealm.Shared.Exceptions;

public class InvalidEmailOrPasswordException : Exception
{
    public InvalidEmailOrPasswordException() : base("Invalid email or password.")
    {
    }
}