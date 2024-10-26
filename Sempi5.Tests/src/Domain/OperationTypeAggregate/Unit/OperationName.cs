using System;
using Sempi5.Domain.OperationTypeAggregate;
using Xunit;

namespace Sempi5.Tests.Domain.OperationTypeAggregate.Unit;

public class OperationNameTest
{
    [Fact]
    public void TestValidContructor()
    {
        var obj = new OperationName("Teste");
        
        Assert.NotNull(obj);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void TestInvalidContructor(string name)
    {
        Assert.Throws<ArgumentException>(() => new OperationName(name));
    }
    
    [Fact]
    public void TestToString()
    {
        var obj = new OperationName("Teste");
        
        Assert.Equal("Teste", obj.ToString());
    }
    
}