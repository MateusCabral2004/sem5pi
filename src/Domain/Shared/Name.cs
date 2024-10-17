namespace Sempi5.Domain.Shared;

public class Name
{
    private readonly string _name;
    
    public Name(string name)
    {
        ValidateName(name);
        _name = name;
    }
    
    private void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty.");
        }
    }

    public override string ToString()
    {
        return _name;
    }
}