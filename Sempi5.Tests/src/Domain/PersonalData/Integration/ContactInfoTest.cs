using System;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Xunit;

namespace Sempi5.Tests.Domain.PersonalData.Integration;

public class ContactInfoTest
{
    [Theory]
    [InlineData("emailtest@gmail.com", 987654321)]
    [InlineData("emailtest2@gmail.com", 923456789)]
    public void ConstructorTest(string email, int phoneNumber)
    {
        var emailObj = new Email(email);
        var phoneNumberObj = new PhoneNumber(phoneNumber);
        
        var obj = new ContactInfo(emailObj, phoneNumberObj);
        
        Assert.NotNull(obj);
        Assert.Equal(emailObj, obj.email());
        Assert.Equal(phoneNumberObj, obj.phoneNumber());
    }
    
    [Theory]
    [InlineData("teste@gmail.com", 987654321)]
    [InlineData("test2@gmail.com", 923456789)]
    public void GetEmailTest(string email, int phoneNumber)
    {
        var emailObj = new Email(email);
        var phoneNumberObj = new PhoneNumber(phoneNumber);
        
        var obj = new ContactInfo(emailObj, phoneNumberObj);
        
        Assert.Equal(emailObj, obj.email());
    }
    
    [Theory]
    [InlineData("teste@gmail.com", 987654321)]
    [InlineData("test2@gmail.com", 923456789)]
    public void GetPhoneNumberTest(string email, int phoneNumber)
    {
        var emailObj = new Email(email);
        var phoneNumberObj = new PhoneNumber(phoneNumber);
        
        var obj = new ContactInfo(emailObj, phoneNumberObj);
        
        Assert.Equal(phoneNumberObj, obj.phoneNumber());
    }
}