using System.Text.RegularExpressions;

namespace Sempi5.Domain.Shared;

public class Email
{
    private readonly string _emailAddress;
    
    public Email(string emailAddress)
    {
        Console.WriteLine("Creating email object with address: " + emailAddress + "...");
        ValidateEmailAddress(emailAddress);
        _emailAddress = emailAddress;

    }
    
    private void ValidateEmailAddress(string emailAddress)
    {
        if (emailAddress == null)
        {
            throw new ArgumentNullException("Email address cannot be null.");
        }

        if (emailAddress.Length == 0)
        {
            throw new ArgumentException("Email address cannot be empty.");
        }

        if (!emailAddress.Contains('@'))
        {
            throw new ArgumentException("Email address must contain an @ symbol.");
        }
        /*        
        if(Regex.IsMatch(emailAddress, @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]+$"))
        {
            throw new ArgumentException("Invalid email format");
        }
        */
    }

    public override string ToString()
    {
        return _emailAddress;
    }
    
    public bool Equals(Email email)
    {
        return email._emailAddress == _emailAddress;
    }
    
}