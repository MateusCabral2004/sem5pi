using System;
using Sempi5.Domain.StaffAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.StaffAggregate.Unit;

public class LicenseNumberTest
{

    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    public void ConstructorWithValidParameters(int licenseNumber)
    { 
        
        var obj = new LicenseNumber(licenseNumber);
        
        Assert.NotNull(obj);
        Assert.Equal(licenseNumber, obj.licenseNumber());
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void ConstructorWithInvalidParameters(int licenseNumber)
    {
        
        Assert.Throws<ArgumentException>(() => new LicenseNumber(licenseNumber));
        
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    public void ToStringTest(int licenseNumber)
    {
        
        var obj = new LicenseNumber(licenseNumber);
        
        Assert.Equal(licenseNumber.ToString(), obj.ToString());
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    public void EqualsTest(int licenseNumber)
    {
        
        var obj = new LicenseNumber(licenseNumber);
        var obj2 = new LicenseNumber(licenseNumber);
        
        Assert.True(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(10, 20)]
    [InlineData(20, 30)]
    public void NotEqualsTest(int licenseNumber, int licenseNumber2)
    {
        
        var obj = new LicenseNumber(licenseNumber);
        var obj2 = new LicenseNumber(licenseNumber2);
        
        Assert.False(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    public void NotEqualsNullTest(int licenseNumber)
    {
        
        var obj = new LicenseNumber(licenseNumber);
        
        Assert.False(obj.Equals(null));
    }
}