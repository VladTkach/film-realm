namespace FilmRealm.Shared.Exceptions;

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(string entityName) : base($"{entityName} already exist")
    {
    }
}