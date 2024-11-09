namespace Sempi5.Domain.SpecializationAggregate.SpecializationExceptions;

public class SpecializationNotFoundException : Exception
{
    public SpecializationNotFoundException(string message) : base(message)
    {
    }
}