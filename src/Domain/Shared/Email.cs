namespace Sempi5.Domain.Shared;

public class Email
{
    private readonly string _emailAddress;


    public Email(string emailAddress)
    {

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

        //TODO: Add More Validations For Email Logic
    }

    public override string ToString()
    {
        return _emailAddress;

    }
    
}