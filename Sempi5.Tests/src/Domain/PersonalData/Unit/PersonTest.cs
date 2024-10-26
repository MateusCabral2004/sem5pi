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
    public void ConstructorTest(string firstName, string lastName, string email, int phoneNumber)
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
}