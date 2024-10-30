using System;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace Sempi5.Tests.Domain.Shared;

public class EmailTest
{
    [Theory]
    [InlineData("mateus@gmail.com")]
    [InlineData("1220704@isep.ipp.pt")]
    [InlineData("abc2@abs.com")]
    [InlineData("john.doe@example.com")]
    [InlineData("jane-doe@company.org")]
    [InlineData("user_123@domain.co.uk")]
    [InlineData("first.last@subdomain.example.com")]
    [InlineData("simple@example.com")]
    [InlineData("customer-service@store.shop")]
    [InlineData("user+alias@domain.com")]
    [InlineData("test123@edu.institute.ac.in")]
    [InlineData("person@123domain.org")]
    [InlineData("email@domain.museum")]
    public void TestConstructorWithValidEmails(string email)
    {
        // Act
        var emailObject = new Email(email);
        // Assert
        Assert.AreEqual(email, emailObject.ToString());
    }
    
    [Theory]
    [InlineData("plainaddress")]                     // Missing "@" and domain
    [InlineData("@missingusername.com")]             // Missing username before "@"
    [InlineData("username@.com")]                    // Missing domain name
    [InlineData("username@domain..com")]             // Double dot in domain
    [InlineData("username@domain.c")]                // Domain part too short
    [InlineData("username@-domain.com")]             // Hyphen at the start of the domain
    [InlineData("username@domain.com-")]             // Hyphen at the end of the domain
    [InlineData("username@.domain.com")]             // Dot immediately after "@" 
    [InlineData("username@domain.c@om")]             // Two "@" symbols
    [InlineData("username@domain.c..o")]             // Multiple dots in domain suffix
    [InlineData("user name@domain.com")]             // Space in username
    [InlineData("username@domain-.com")]             // Hyphen at the end of the domain
    [InlineData("username@domain.c@o.m")]           // Multiple "@" symbols
    public void TestConstructorWithInvalidEmails(string email)
    {
        // Act
        Assert.ThrowsException<ArgumentException>(() => new Email(email));
    }
    
    [Fact]
    public void TestConstructorWithNullEmails()
    {
        // Act
        Assert.ThrowsException<ArgumentNullException>(() => new Email(null));
    }
    
    [Fact]
    public void TestConstructorWithEmptyEmails()
    {
        // Act
        Assert.ThrowsException<ArgumentException>(() => new Email(""));
    }

    [Fact]
    public void TestEqualsMethod()
    {
        // Arrange
        var email1 = new Email("mateus@gmail.com");
        var email2 = new Email("mateus@gmail.com");

        // Act
        var result = email1.Equals(email2);

        // Assert
        Assert.IsTrue(result);
    }

    [Fact]
    public void TestEqualsMethodWithDifferentEmails()
    {
        // Arrange
        var email1 = new Email("mateus@gmail.com");
        var email2 = new Email("sandro@gmail.com");

        // Act
        var result = email1.Equals(email2);

        // Assert
        Assert.IsFalse(result);
    }

    [Fact]
    public void TestEqualsMethodWithNullEmail()
    {
        // Arrange
        var email1 = new Email("mateus@gmail.com");

        // Act
        var result = email1.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    [Fact]
    public void TestToStringMethod()
    {
        // Arrange
        string emailString = "mateus@gmai.com";
        var email = new Email(emailString);

        // Act
        var result = email.ToString();

        // Assert
        Assert.AreEqual(emailString, result);
    }
}