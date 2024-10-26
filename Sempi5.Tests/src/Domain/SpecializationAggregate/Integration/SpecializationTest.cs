using System;
using Xunit;
using Sempi5.Domain.SpecializationAggregate;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SpecializationAggregate.Integration;

public class SpecializationTest
{
    [Fact]
    public void ConstructorTest()
    {
        // Arrange
        var specializationName = new SpecializationName("Cardiology");
        
        // Act
        var obj = new Specialization(specializationName);
        
        // Assert
        Assert.NotNull(obj);
        Assert.Equal(specializationName, obj.specializationName);
    }
    
    [Fact]
    public void EqualsTest()
    {
        // Arrange
        var name = "Cardiology";
        var specializationName1 = new SpecializationName(name);
        var specializationName2 = new SpecializationName(name);

        // Act
        var obj1 = new Specialization(specializationName1);
        var obj2 = new Specialization(specializationName2);
        
        // Assert
        Assert.True(obj1.Equals(obj2));
    }
    
    [Theory]
    [InlineData("Cardiology", "Neurology")]
    [InlineData("Operator", "Doctor")]
    public void NotEqualsTest(string name1, string name2)
    {
        // Arrange
        var specializationName1 = new SpecializationName(name1);
        var specializationName2 = new SpecializationName(name2);

        var obj1 = new Specialization(specializationName1);
        var obj2 = new Specialization(specializationName2);
        
        // Assert
        Assert.False(obj1.Equals(obj2));
    }
}