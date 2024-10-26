using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.ConfirmationLink;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.ConfirmationToken.Unit;


public class ConfirmationLinkTest
{

    [Fact]
    public void TestConstructorWithValidParameters()
    {

        var mockEmail = new Mock<Email>("teste@gmail.com");

        var confirmationLink = new ConfirmationLink(mockEmail.Object);
        
        Assert.NotNull(confirmationLink);
        Assert.Equal(mockEmail.Object, confirmationLink.email);
        Assert.False(confirmationLink.IsUsed);
        Assert.True(confirmationLink.ExpiryDate > DateTime.Now);

    }

    [Fact]
    public void TestConstructorWithInvalidParameters()
    {
        var mockEmail = new Mock<Email>("teste@gmail.com");

        var confirmationLink = new ConfirmationLink(mockEmail.Object);
        
        Assert.Throws<ArgumentNullException>(() => new ConfirmationLink(null));
    }

    [Fact]
    public void TestResetExpiryDate()
    {
        var mockEmail = new Mock<Email>("teste@gmail.com");

        var confirmationLink = new ConfirmationLink(mockEmail.Object);

        var expiryDate = confirmationLink.ExpiryDate;
        
        confirmationLink.ResetExpiryDate();
        
        Assert.True(confirmationLink.ExpiryDate > expiryDate);
    }
}