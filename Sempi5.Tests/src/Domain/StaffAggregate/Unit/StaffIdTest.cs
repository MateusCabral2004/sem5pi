using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.StaffAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.StaffAggregate.Unit;


public class StaffIdTest
{

    [Theory]
    [InlineData("A202400001")]
    [InlineData("A202400002")]
    public void ConstructorTest(string staffId)
    {
        var obj = new StaffId(staffId);
        
        Assert.NotNull(obj);
        Assert.Equal(staffId, obj.Value);
    }
    
    [Theory]
    [InlineData("A202400001")]
    [InlineData("A202400002")]
    public void ToStringTest(string staffId)
    {
        
        var obj = new StaffId(staffId);
        
        Assert.Equal(staffId, obj.ToString());
    }
    
    [Theory]
    [InlineData("A202400001")]
    [InlineData("A202400002")]
    public void EqualsTest(string staffId)
    {
        
        var obj = new StaffId(staffId);
        var obj2 = new StaffId(staffId);
        
        Assert.True(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData("A202400001", "A202400002")]
    [InlineData("A202400002", "A202400003")]
    public void NotEqualsTest(string staffId, string staffId2)
    {
        
        var obj = new StaffId(staffId);
        var obj2 = new StaffId(staffId2);
        
        Assert.False(obj.Equals(obj2));
    }
    
    [Theory]
    [InlineData("A202400001")]
    [InlineData("A202400002")]
    public void NotEqualsNullTest(string staffId)
    {
        
        var obj = new StaffId(staffId);
        
        Assert.False(obj.Equals(null));
    }
    
    [Theory]
    [InlineData("A202400001")]
    [InlineData("A202400002")]
    public void AsStringTest(string staffId)
    {
        
        var obj = new StaffId(staffId);
        
        Assert.Equal(staffId, obj.AsString());
    }
    
    
    
}