namespace Sempi5.Domain.Shared;

public class Name : IValueObject
{
    public string _name;
    
    private Name () { } 
    
    public Name(string name)
    {
        ValidateName(name);
        _name = name;
    }
    
    private void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("ERRO");
            throw new ArgumentException("Name cannot be null or empty.");
        }
    }

    public override string ToString()
    {
        return _name;
    }
    
    public bool Equals(Name name)
    {
        if(name == null)
        {
            return false;
        }
        
        return name._name.ToLower().Equals(_name.ToLower());
    }
}