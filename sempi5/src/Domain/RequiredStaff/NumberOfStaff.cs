namespace Sempi5.Domain.OperationType;

public class NumberOfStaff
{
    public int value;

    private NumberOfStaff() {}
    
    public NumberOfStaff(int value)
    {
        this.value = value;
    }

    public int getValue()
    {
        return value;
    }
}