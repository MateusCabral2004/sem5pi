using System;
using Moq;
using Xunit;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;

public class ConfirmationTokenUnitTests
{
    [Fact]
    public void TestConstructorWithValidParamethers()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        ConfirmationToken token = new ConfirmationToken(email.Object);
        Assert.NotNull(token);
        Assert.False(token.IsUsed);
        Assert.True(token.ExpiryDate > DateTime.Now);
        Assert.Equal(email.Object, token.email);
    }
    
    [Fact]
    public void TestConstructorWithNullEmail()
    {
        Assert.Throws<ArgumentNullException>(() => new ConfirmationToken(null));
    }
    
    [Fact]
    public void TestExpiryDateInConstructor()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        ConfirmationToken token = new ConfirmationToken(email.Object);
        var expectedExpiryDate = DateTime.Now.AddDays(1);
        Assert.True((token.ExpiryDate - expectedExpiryDate).TotalSeconds < 1);
    }
    
    [Fact]
    public void TestUse()
    {
        Mock<Email> email = new Mock<Email>("example@gmail.com");
        ConfirmationToken token = new ConfirmationToken(email.Object);
        token.Use();
        Assert.True(token.IsUsed);
    }
}
