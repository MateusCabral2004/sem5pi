using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.User.IntegrationTests;


public class SystemUserIntegrationTest
{
    [Fact]
    public void ConstructorWithValidParamethers()
    {
        // Arrange
        var email = new Email("user@example.com");
        var role = "UserRole";

        // Act
        var user = new SystemUser(email, role);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
        Assert.Equal("user@example.com", user.Username);
        Assert.Equal(role, user.Role);
        Assert.False(user.IsVerified);
    }

    [Fact]
    public void ConstructorWithValidParamethersAndIsVerified()
    {
        // Arrange
        var email = new Email("user@example.com");
        var role = "UserRole";
        var isVerified = true;

        // Act
        var user = new SystemUser(email, role, isVerified);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
        Assert.Equal("user@example.com", user.Username);
        Assert.Equal(role, user.Role);
        Assert.True(user.IsVerified);
    }
    
    [Fact]
    public void ConstructorWithNullEmail()
    {
        // Arrange
        var role = "UserRole";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(null, role));
    }
    
    [Fact]
    public void ConstructorWithNullEmailAndIsVerified()
    {
        // Arrange
        var role = "UserRole";
        var isVerified = true;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(null, role, isVerified));
    }

    [Fact]
    public void ConstructorWithNullRole()
    {
        // Arrange
        var email = new Email("user@example.com");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(email, null));

    }

    [Fact]
    public void ConstructorWithNullRoleAndIsVerified()
    {
        var email = new Email("user@example.com");
        var isVerified = true;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(email, null, isVerified));
    }

    [Fact]
    public void ConstructorWithEmptyRole()
    {
        // Arrange
        var email = new Email("user@example.com");

        // Act && Assert
        Assert.Throws<ArgumentException>(() => new SystemUser(email, ""));
    }

    [Fact]
    public void ConstructorWithEmptyRoleAndIsVerified()
    {
        // Arrange
        var mockEmail = new Mock<Email>("user@example.com");

        // Act && Assert
        Assert.Throws<ArgumentException>(() => new SystemUser(mockEmail.Object, ""));
    }

    [Fact]
    public void Verify()
    {
        // Arrange
        var mockEmail = new Mock<Email>("user@example.com");
        var role = "UserRole";
        var user = new SystemUser(mockEmail.Object, role);

        // Act
        user.Verify();

        // Assert
        Assert.True(user.IsVerified);
    }
}