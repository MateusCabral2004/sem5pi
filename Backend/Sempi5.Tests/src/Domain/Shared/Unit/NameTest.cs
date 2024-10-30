using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.Shared.Unit;

public class NameTest
{

    [Theory]
    [InlineData("Mateus")]
    [InlineData("João")]
    [InlineData("Maria João")]
    [InlineData("Sandro Luís")]
    public void TestConstructorWithValidNames(string name)
    {
        // Act
        var nameObject = new Name(name);
        // Assert
        Assert.NotNull(nameObject);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void TestConstructorWithInvalidNames(string name)
    {
        // Act
        Assert.Throws<ArgumentException>(() => new Name(name));
    }
    
    [Fact]
    public void TestToString()
    {
        // Arrange
        var name = "Mateus";
        var nameObject = new Name(name);
        // Act
        var result = nameObject.ToString();
        // Assert
        Assert.Equal(name, result);
    }
    
    [Fact]
    public void TestEqualsWithEqualNames()
    {
        // Arrange
        var name1 = new Name("Mateus");
        var name2 = new Name("Mateus");
        // Act
        var result = name1.Equals(name2);
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestEqualsWithDifferentNames()
    {
        // Arrange
        var name1 = new Name("Mateus");
        var name2 = new Name("João");
        // Act
        var result = name1.Equals(name2);
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TestEqualsWithNullName()
    {
        // Arrange
        var name1 = new Name("Mateus");
        // Act
        var result = name1.Equals(null);
        // Assert
        Assert.False(result);
    }
}