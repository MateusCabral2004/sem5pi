using System;
using Sempi5.Domain.ConfirmationLinkAggregate;
using Sempi5.Domain.Shared;
using Xunit;

namespace Sempi5.Tests.Domain.ConfirmationTokenTests.IntegrationTests;

public class ConfirmationLinkIntegrationTest
{
    [Fact]
    public void TestConstructorWithValidParameters()
    {
        var email = new Email("teste@gmail.com");
        var confirmationLink = new ConfirmationLink(email);
    
        Assert.NotNull(confirmationLink);
        Assert.Equal(email, confirmationLink.email); // Certifique-se de que a propriedade está nomeada corretamente
        Assert.False(confirmationLink.IsUsed);
        Assert.True(confirmationLink.ExpiryDate > DateTime.Now);
    }

    [Fact]
    public void TestConstructorWithInvalidParameters()
    {
        Assert.Throws<ArgumentNullException>(() => new ConfirmationLink(null));
    }

    [Fact]
    public void TestResetExpiryDate()
    {
        var email = new Email("teste@gmail.com");
        var confirmationLink = new ConfirmationLink(email);

        var expiryDate = confirmationLink.ExpiryDate;
    
        confirmationLink.ResetExpiryDate();
    
        Assert.True(confirmationLink.ExpiryDate > expiryDate);
    }

}