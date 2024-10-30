using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.SurgeryRoomAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SurgeryRoomAggregate.Unit;

public class RoomNumberTest
{

    [Fact]
    public void TestConstructorWithValidParameters()
    {
        // Arrange
        var roomNumber = new RoomNumber(1);

        // Act
        var result = roomNumber.AsInt();

        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void TestAsInt()
    {
        // Arrange
        var roomNumber = new RoomNumber(1);

        // Act
        var result = roomNumber.AsInt();

        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void TestAsString()
    {
        // Arrange
        var roomNumber = new RoomNumber(1);

        // Act
        var result = roomNumber.AsString();

        // Assert
        Assert.Equal("1", result);
    }
    
    
}