using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.SpecializationAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SpecializationAggregate.Unit;


public class SpecializationIDTest
{

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void ConstructorWithValidParameters(long id)
    {
        
        var obj = new SpecializationID(id);
        
        Assert.NotNull(obj);
        Assert.Equal(id, obj.AsLong());
        
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void AsStringTest(long id)
    {
        
        var obj = new SpecializationID(id);
        
        Assert.Equal(id.ToString(), obj.AsString());
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void EqualsTest(long id)
    {
        
        var obj = new SpecializationID(id);
        var obj2 = new SpecializationID(id);
        
        Assert.True(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void NotEqualsTest(long id, long id2)
    {
        
        var obj = new SpecializationID(id);
        var obj2 = new SpecializationID(id2);
        
        Assert.False(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void NotEqualsNullTest(long id)
    {
        
        var obj = new SpecializationID(id);
        
        Assert.False(obj.Equals(null));
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void AsLongTest(long id)
    {
        
        var obj = new SpecializationID(id);
        
        Assert.Equal(id, obj.AsLong());
    }
    
}