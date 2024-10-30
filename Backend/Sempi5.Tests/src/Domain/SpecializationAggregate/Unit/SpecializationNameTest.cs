using System;
using Sempi5.Domain.SpecializationAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SpecializationAggregate.Unit;

public class SpecializationNameTest
{

    [Theory]
    [InlineData("Cardiology")]
    [InlineData("Neurology")]
    public void ConstructorWithValidParametersTest(string name)
    {
        var obj = new SpecializationName(name);
        
        Assert.NotNull(obj);
        Assert.Equal(name, obj.ToString());
    }
    
    [Fact]
    public void ConstructorWithInvalidParametersTest()
    {
        Assert.Throws<ArgumentException>(() => new SpecializationName(""));
    }
    
    [Theory]
    [InlineData("Cardiology")]
    [InlineData("Neurology")]
    public void ToStringTest(string name)
    {
        
        var obj = new SpecializationName(name);
        
        Assert.Equal(name, obj.ToString());
    }
    
    [Theory]
    [InlineData("Cardiology")]
    [InlineData("Neurology")]
    public void EqualsTest(string name)
    {
        
        var obj = new SpecializationName(name);
        var obj2 = new SpecializationName(name);
        
        Assert.True(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData("Cardiology", "Neurology")]
    [InlineData("Neurology", "Cardiology")]
    public void NotEqualsTest(string name, string name2)
    {
        
        var obj = new SpecializationName(name);
        var obj2 = new SpecializationName(name2);
        
        Assert.False(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData("Cardiology")]
    [InlineData("Neurology")]
    public void NotEqualsNullTest(string name)
    {
        
        var obj = new SpecializationName(name);
        
        Assert.False(obj.Equals(null));
    }
    
}