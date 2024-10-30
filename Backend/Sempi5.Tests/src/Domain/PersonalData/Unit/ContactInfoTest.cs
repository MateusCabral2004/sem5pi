using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.PersonalData.Unit;

public class ContactInfoTest
{
    [Theory]
    [InlineData("emailtest@gmail.com", 987654321)]
    [InlineData("emailtest2@gmail.com", 923456789)]
    public void ConstructorTest(string email, int phoneNumber)
    {
        
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        
        var obj = new ContactInfo(mockEmail.Object, mockPhoneNumber.Object);
        
        Assert.NotNull(obj);
        Assert.Equal(mockEmail.Object, obj.email());
        Assert.Equal(mockPhoneNumber.Object, obj.phoneNumber());
        
    }
    
    [Theory]
    [InlineData("teste@gmail.com", 987654321)]
    [InlineData("test2@gmail.com", 923456789)]
    public void GetEmailTest(string email, int phoneNumber)
    {
        
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        
        var obj = new ContactInfo(mockEmail.Object, mockPhoneNumber.Object);
        
        Assert.Equal(mockEmail.Object, obj.email());
        
    }
    
    [Theory]
    [InlineData("teste@gmail.com", 987654321)]
    [InlineData("test2@gmail.com", 923456789)]
    public void GetPhoneNumberTest(string email, int phoneNumber)
    {
        
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        
        var obj = new ContactInfo(mockEmail.Object, mockPhoneNumber.Object);
        
        Assert.Equal(mockPhoneNumber.Object, obj.phoneNumber());
        
    }
}