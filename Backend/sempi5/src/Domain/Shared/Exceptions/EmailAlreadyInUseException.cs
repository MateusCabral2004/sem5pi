namespace Sempi5.Domain.Shared.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(string message) : base(message)
    {
    }
}