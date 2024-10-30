using System;
using Sempi5.Domain.PersonalData;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.PersonalData.Unit;

public class PhoneNumberTest
{

    [Theory]
    [InlineData(987654321)]
    [InlineData(923456789)]
    public void ConstructorWithValidParameters(int phoneNumber)
    {
        
        var obj = new PhoneNumber(phoneNumber);
        
        Assert.NotNull(obj);
        Assert.Equal(phoneNumber, obj.phoneNumber());
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void ConstructorWithInvalidParameters(int phoneNumber)
    {
        
        Assert.Throws<ArgumentException>(() => new PhoneNumber(phoneNumber));
        
    }
    
    [Theory]
    [InlineData(987654321)]
    [InlineData(923456789)]
    public void GetPhoneNumberTest(int phoneNumber)
    {
        
        var obj = new PhoneNumber(phoneNumber);
        
        Assert.Equal(phoneNumber, obj.phoneNumber());
    }
    
    [Theory]
    [InlineData(987654321)]
    [InlineData(923456789)]
    public void EqualsTest(int phoneNumber)
    {
        
        var obj = new PhoneNumber(phoneNumber);
        var obj2 = new PhoneNumber(phoneNumber);
        
        Assert.True(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(987654321, 923456789)]
    [InlineData(923456789, 987654321)]
    public void NotEqualsTest(int phoneNumber, int phoneNumber2)
    {
        
        var obj = new PhoneNumber(phoneNumber);
        var obj2 = new PhoneNumber(phoneNumber2);
        
        Assert.False(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(987654321)]
    [InlineData(923456789)]
    public void NotEqualsNullTest(int phoneNumber)
    {
        
        var obj = new PhoneNumber(phoneNumber);
        
        Assert.False(obj.Equals(null));
    }
}