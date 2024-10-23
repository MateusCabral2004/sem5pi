using Sempi5.Domain.Shared;

namespace Sempi5.Domain.OperationType;

public class OperationName : IValueObject
{
    public string name;

    private OperationName() {}
    
    public OperationName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        this.name = name;
    }

    public override string ToString()
    {
        return name;
    }

}