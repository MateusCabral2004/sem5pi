using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.User.UnitTests;

public class SystemUserUnitTest
{
    [Fact]
    public void ConstructorWithValidParameters()
    {
        // Arrange
        string email = "user@example.com";
        var mockEmail = new Mock<Email>(email);
        mockEmail.Setup(e => e.ToString()).Returns(email);
        var role = "UserRole";

        // Act
        var user = new SystemUser(mockEmail.Object, role);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(mockEmail.Object, user.Email);
        Assert.Equal("user@example.com", user.Username);
        Assert.Equal(role, user.Role);
        Assert.False(user.IsVerified);
    }

    [Fact]
    public void ConstructorWithValidParametersAndIsVerified()
    {
        // Arrange
        string email = "user@example.com";
        var mockEmail = new Mock<Email>(email);
        mockEmail.Setup(e => e.ToString()).Returns(email);
        var role = "UserRole";
        var isVerified = true;

        // Act
        var user = new SystemUser(mockEmail.Object, role, isVerified);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(mockEmail.Object, user.Email);
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
        var mockEmail = new Mock<Email>("user@example.com");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(mockEmail.Object, null));

    }

    [Fact]
    public void ConstructorWithNullRoleAndIsVerified()
    {
        var mockEmail = new Mock<Email>("user@example.com");
        var isVerified = true;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SystemUser(mockEmail.Object, null, isVerified));
    }

    [Fact]
    public void ConstructorWithEmptyRole()
    {
        // Arrange
        var mockEmail = new Mock<Email>("user@example.com");

        // Act && Assert
        Assert.Throws<ArgumentException>(() => new SystemUser(mockEmail.Object, ""));
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