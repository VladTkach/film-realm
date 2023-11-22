namespace FilmRealm.Shared.Exceptions;

public class LargeFileException : Exception
{
    public LargeFileException(string size) : base($"File must be less then {size}")
    {
    }
}