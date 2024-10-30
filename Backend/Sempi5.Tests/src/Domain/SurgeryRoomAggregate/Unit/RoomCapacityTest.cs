using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.SurgeryRoomAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SurgeryRoomAggregate.Unit;

public class RoomCapacityTest
{
    [Fact]
    public void ConstructorWithValidParameters()
    {
        // Arrange
        int capacity = 10;

        // Act
        var roomCapacity = new RoomCapacity(capacity);
        
        // Assert
        Assert.Equal(capacity, roomCapacity.Capacity);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstructorWithInvalidParameters(int capacity)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new RoomCapacity(capacity));
    }
    
    [Fact]
    public void TestAsInt()
    {
        // Arrange
        int capacity = 10;
        var roomCapacity = new RoomCapacity(capacity);
        
        // Act
        var result = roomCapacity.AsInt();
        
        // Assert
        Assert.Equal(capacity, result);
    }
}