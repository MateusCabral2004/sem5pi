using System.Linq.Expressions;

namespace Sempi5.Domain.Shared.Exceptions;

public class InvalidEmailFormatException : Exception
{
    public InvalidEmailFormatException(string message) : base(message)
    {
    }
}