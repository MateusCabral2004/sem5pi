using System;
using Moq;
using Xunit;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;

public class ConfirmationTokenIntegrationTests
{
    [Fact]
    public void TestConstructorWithValidParamethers()
    {
        var email = new Email("example@gmail.com");
        var token = new ConfirmationToken(email, "id");
        Assert.NotNull(token);
        Assert.False(token.IsUsed);
        Assert.True(token.ExpiryDate > DateTime.Now);
        Assert.Equal(email, token.email);
    }
    
    [Fact]
    public void TestConstructorWithNullEmail()
    {
        Assert.Throws<ArgumentNullException>(() => new ConfirmationToken(null, "id"));
    }
    
    [Fact]
    public void TestExpiryDateInConstructor()
    {
        var email = new Email("example@gmail.com");
        var token = new ConfirmationToken(email, "id");
        var expectedExpiryDate = DateTime.Now.AddDays(1);
        Assert.True((token.ExpiryDate - expectedExpiryDate).TotalSeconds < 1);
    }


    [Fact]
    public void TestUse()
    {
        var email = new Email("example@gmail.com");
        var token = new ConfirmationToken(email, "id");
        token.Use();
        Assert.True(token.IsUsed);
    }
}