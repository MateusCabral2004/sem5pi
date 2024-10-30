using Sempi5.Domain.Shared;

namespace Sempi5.Domain.StaffAggregate;

public class LicenseNumber :IValueObject
{
    
    private readonly int _licenseNumber;
    
    public LicenseNumber(int licenseNumber)
    {
        ValidateLicenseNumber(licenseNumber);
        
        _licenseNumber = licenseNumber;
    }
    
    private void ValidateLicenseNumber(int licenseNumber)
    {
        if (licenseNumber <= 0)
        {
            throw new ArgumentException("License number cannot be negative or equals to 0.");
        }
        
    }
    
    public override string ToString()
    {
        return _licenseNumber.ToString();
    }
    
    public virtual int licenseNumber()
    {
        return _licenseNumber;
    }
    
    public bool Equals(LicenseNumber licenseNumber)
    {
        if(licenseNumber == null)
        {
            return false;
        }
        
        return licenseNumber._licenseNumber == _licenseNumber;
    }
}