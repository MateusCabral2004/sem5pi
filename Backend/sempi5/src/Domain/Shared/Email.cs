using System.Text.RegularExpressions;
using Sempi5.Domain.Shared.Exceptions;

namespace Sempi5.Domain.Shared;

public class Email : IValueObject
{
    private readonly string _emailAddress;

    public Email(string emailAddress)
    {
        ValidateEmailAddress(emailAddress);
        _emailAddress = emailAddress;
    }

    private void ValidateEmailAddress(string emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);

        if (emailAddress.Length == 0)
        {
            throw new ArgumentException("Email address cannot be empty.");
        }

        if (!emailAddress.Contains('@'))
        {
            throw new InvalidEmailFormatException("Email address must contain an @ symbol.");
        }

        if (!Regex.IsMatch(emailAddress, @"^[\w.+-]+@(?<!-)(?!.*\.\.)([a-zA-Z\d]+[-a-zA-Z\d]*[a-zA-Z\d]+)(\.[a-zA-Z]{2,})+$"))
        {
            throw new InvalidEmailFormatException("Invalid email format.");
        }
    }

    public override string ToString()
    {
        return _emailAddress;
    }

    public bool Equals(Email email)
    {
        if (email == null)
        {
            return false;
        }
        
        return email._emailAddress == _emailAddress;
    }
}