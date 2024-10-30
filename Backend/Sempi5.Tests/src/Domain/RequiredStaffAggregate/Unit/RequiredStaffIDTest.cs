using JetBrains.Annotations;
using Sempi5.Domain.RequiredStaffAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.RequiredStaffAggregate.Unit;

public class RequiredStaffIDTest
{

    [Fact]
    public void TestConstructorWithValidParameters()
    {
        // Arrange
        long value = 12345;

        // Act
        var requiredStaffId = new RequiredStaffID(value);

        // Assert
        Assert.Equal(value, requiredStaffId.AsLong());
    }

    [Fact]
    public void TestMethodAsLong()
    {
        // Arrange
        long value = 67890;
        var requiredStaffId = new RequiredStaffID(value);

        // Act
        var result = requiredStaffId.AsLong();

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void TestMethodAsString()
    {
        // Arrange
        long value = 123456789;
        var requiredStaffId = new RequiredStaffID(value);

        // Act
        var result = requiredStaffId.AsString();

        // Assert
        Assert.Equal(value.ToString(), result);
    }
    
}