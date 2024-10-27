using System;
using Moq;
using Sempi5.Domain.ConfirmationTokenAggregate;
using Xunit;
using Sempi5.Domain.Shared;

namespace Sempi5.Tests.Domain.ConfirmationTokenTests.UnitTests;

public class ConfirmationTokenUnitTests
{
    [Fact]
    public void TestConstructorWithValidParamethers()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        var token = new ConfirmationToken(email.Object, "id");
        Assert.NotNull(token);
        Assert.False(token.IsUsed);
        Assert.True(token.ExpiryDate > DateTime.Now);
        Assert.Equal(email.Object, token.email);
    }
    
    [Fact]
    public void TestConstructorWithNullEmail()
    {
        Assert.Throws<ArgumentNullException>(() => new ConfirmationToken(null, "id"));
    }
    
    [Fact]
    public void TestExpiryDateInConstructor()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        ConfirmationToken token = new ConfirmationToken(email.Object, "id");
        var expectedExpiryDate = DateTime.Now.AddDays(1);
        Assert.True((token.ExpiryDate - expectedExpiryDate).TotalSeconds < 1);
    }
    
    [Fact]
    public void TestUse()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        ConfirmationToken token = new ConfirmationToken(email.Object,"id");
        token.Use();
        Assert.True(token.IsUsed);
    }
}
