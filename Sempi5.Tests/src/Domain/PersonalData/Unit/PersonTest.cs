using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.PersonalData.Unit;

public class PersonTest
{

    [Theory]
    [InlineData("John", "Doe", "teste@gmail.com", 987654321)]
    [InlineData("Jane", "Doe", "teste@gmail.com", 923456789)]
    public void ConstructorWithValidParametersTest(string firstName, string lastName, string email, int phoneNumber)
    {
        
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockFirstName = new Mock<Name>(firstName);
        var mockLastName = new Mock<Name>(lastName);
        
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        mockContactInfo.Setup(c => c.email()).Returns(mockEmail.Object);
        mockContactInfo.Setup(c => c.phoneNumber()).Returns(mockPhoneNumber.Object);
        mockLastName.Setup(l => l.ToString()).Returns(lastName);
        mockFirstName.Setup(f => f.ToString()).Returns(firstName);
        
        
        var obj = new Person(mockFirstName.Object, mockLastName.Object, mockContactInfo.Object);
        
        Assert.NotNull(obj);
        Assert.Equal(firstName, obj.FirstName.ToString());
        Assert.Equal(lastName, obj.LastName.ToString());
        Assert.Equal(email, obj.ContactInfo.email().ToString());
        Assert.Equal(phoneNumber, obj.ContactInfo.phoneNumber().phoneNumber());

    }
    
    [Theory]
    [InlineData( "Doe", "teste@gmail.com", 987654321)]
    [InlineData( "Doe", "teste@gmail.com", 923456789)]
    public void ConstructorWithInvalidFirstNameTest( string lastName, string email, int phoneNumber)
    {
        
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockLastName = new Mock<Name>(lastName);
        
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        mockContactInfo.Setup(c => c.email()).Returns(mockEmail.Object);
        mockContactInfo.Setup(c => c.phoneNumber()).Returns(mockPhoneNumber.Object);
        mockLastName.Setup(l => l.ToString()).Returns(lastName);
        
        Assert.Throws<ArgumentNullException>(() => new Person(null, mockLastName.Object, mockContactInfo.Object));

    }
    
    [Theory]
    [InlineData( "Doe", "teste@gmail.com", 987654321)]
    [InlineData( "Doe", "teste@gmail.com", 923456789)]
    public void ConstructorWithInvalidLastNameTest( string first, string email, int phoneNumber)
    {
        var mockFirstName = new Mock<Name>(first);
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        
        
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        mockContactInfo.Setup(c => c.email()).Returns(mockEmail.Object);
        mockContactInfo.Setup(c => c.phoneNumber()).Returns(mockPhoneNumber.Object);
        
        
        Assert.Throws<ArgumentNullException>(() => new Person(mockFirstName.Object, null, mockContactInfo.Object));

    }
    
    [Theory]
    [InlineData( "Jhon", "Doe")]
    [InlineData( "Jane", "Doe" )]
    public void ConstructorWithInvalidContactInfoTest( string first, string lastName)
    {
        var mockFirstName = new Mock<Name>(first);
        var mockLastName = new Mock<Name>(lastName);
        
        
        mockLastName.Setup(l => l.ToString()).Returns(lastName);
        mockFirstName.Setup(f => f.ToString()).Returns(first);
        
        
        Assert.Throws<ArgumentNullException>(() => new Person(mockFirstName.Object, mockLastName.Object, null));

    }
    
    [Fact]
    public void ConstructorWithAllInvalidParameters()
    {
        
        Assert.Throws<ArgumentNullException>(() => new Person(null, null, null));

    }
    
    
    
}