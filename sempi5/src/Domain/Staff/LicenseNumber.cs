namespace Sempi5.Domain.Staff;

public class LicenseNumber
{
    
    private readonly int _licenseNumber;
    
    public LicenseNumber(int licenseNumber)
    {
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
    
    public int licenseNumber()
    {
        return _licenseNumber;
    }
    
    public bool Equals(LicenseNumber licenseNumber)
    {
        return licenseNumber._licenseNumber == _licenseNumber;
    }
}