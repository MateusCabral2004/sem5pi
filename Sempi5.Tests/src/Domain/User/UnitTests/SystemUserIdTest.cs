using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.User;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.User.Unit;

public class SystemUserIdTest
{

    [Fact]
    public void ConstructorWithValidParameter()
    {
        // Arrange
        var id = 10;
        var idObj = new SystemUserId(id);

        // Assert
        Assert.NotNull(idObj);
        Assert.Equal(id, idObj.AsLong());
    }
    
    [Fact]
    public void ConstructorWithInvalidParameter()
    {
        // Arrange
        var id = -10;
        Assert.Throws<ArgumentException>(() => new SystemUserId(id));
    }
    
    [Fact]
    public void AsString()
    {
        // Arrange
        var id = 10;
        var idStr = id + "";
        var idObj = new SystemUserId(id);

        // Assert
        Assert.Equal(idStr, idObj.AsString());
    }
    
    [Fact]
    public void AsLong()
    {
        // Arrange
        var id = 10;
        var idObj = new SystemUserId(id);

        // Assert
        Assert.Equal(id, idObj.AsLong());
    }
}