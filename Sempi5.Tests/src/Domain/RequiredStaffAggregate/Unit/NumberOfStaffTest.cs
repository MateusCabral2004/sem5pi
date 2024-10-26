using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.RequiredStaffAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.RequiredStaffAggregate.Unit;

public class NumberOfStaffTest
{

    [Fact]
    public void TestConstructorWithValidPArameters()
    {
        int numberOfStaff = 5;
        var obj = new NumberOfStaff(numberOfStaff);
        Assert.NotNull(obj);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TestConstructorWithInvalidParameters(int numberOfStaff)
    {
        Assert.Throws<ArgumentException>(() => new NumberOfStaff(numberOfStaff));
    }
    
    [Fact]
    public void TestGetValue()
    {
        int numberOfStaff = 5;
        var obj = new NumberOfStaff(numberOfStaff);
        Assert.Equal(numberOfStaff, obj.getValue());
    }
    
    [Fact]
    public void TestEquals()
    {
        int numberOfStaff = 5;
        var obj = new NumberOfStaff(numberOfStaff);
        var obj2 = new NumberOfStaff(numberOfStaff);
        Assert.True(obj.Equals(obj2));
    }
    
    [Fact]
    public void TestNotEquals()
    {
        int numberOfStaff = 5;
        var obj = new NumberOfStaff(numberOfStaff);
        var obj2 = new NumberOfStaff(numberOfStaff + 1);
        Assert.False(obj.Equals(obj2));
    }

    [Fact]
    public void TestNotEqualsWithNullObject()
    {
        int numberOfStaff = 5;
        var obj = new NumberOfStaff(numberOfStaff);
        Assert.False(obj.Equals(null));
    }
    
}