using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.SpecializationAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.OperationTypeAggregate.Unit;

public class OperationTypeTest
{

    private OperationType createOperationType()
    {
        var opName = new Mock<OperationName>("Teste");
        var setupDuration = TimeSpan.Parse("00:30:00");
        var surgeryDuration = TimeSpan.Parse("02:00:00");
        var cleaningDuration = TimeSpan.Parse("00:30:00");

        return new OperationType(opName.Object, setupDuration, surgeryDuration, cleaningDuration);
    }

    [Fact]
    public void TestValidContructor1()
    {
        var opName = new Mock<OperationName>("Teste");
        var numStaff = new Mock<NumberOfStaff>(2);
        var specializationName = new Mock<SpecializationName>("surgery");
        var specialization = new Mock<Specialization>(specializationName.Object);
        var reqStaff = new Mock<RequiredStaff>(numStaff.Object, specialization.Object);
        var reqStaffList = new List<RequiredStaff> { reqStaff.Object };
        var setupDuration = TimeSpan.Parse("00:30:00");
        var surgeryDuration = TimeSpan.Parse("02:00:00");
        var cleaningDuration = TimeSpan.Parse("00:30:00");
        
        var obj = new OperationType(opName.Object, reqStaffList, setupDuration, surgeryDuration, cleaningDuration);
        
        Assert.NotNull(obj);
    }
    
    [Fact]
    public void TestValidContructor2()
    {
        var opName = new Mock<OperationName>("Teste");
        var setupDuration = TimeSpan.Parse("00:30:00");
        var surgeryDuration = TimeSpan.Parse("02:00:00");
        var cleaningDuration = TimeSpan.Parse("00:30:00");
        
        var obj = new OperationType(opName.Object, setupDuration, surgeryDuration, cleaningDuration);
        
        Assert.NotNull(obj);
    }

    [Fact]
    public void TestAddRequiredStaff()
    {
        var opType = createOperationType();

        var numStaff = new Mock<NumberOfStaff>(2);
        var specializationName = new Mock<SpecializationName>("surgery");
        var specialization = new Mock<Specialization>(specializationName.Object);

        var reqStaff = new Mock<RequiredStaff>(numStaff.Object, specialization.Object);

        opType.AddRequiredStaff(reqStaff.Object);
        
        Assert.Equal(1, opType.RequiredStaff.Count);
    }
    
    [Fact]
    public void TestRemoveRequiredStaff()
    {
        var opType = createOperationType();

        var numStaff = new Mock<NumberOfStaff>(2);
        var specializationName = new Mock<SpecializationName>("surgery");
        var specialization = new Mock<Specialization>(specializationName.Object);

        var reqStaff = new Mock<RequiredStaff>(numStaff.Object, specialization.Object);

        opType.AddRequiredStaff(reqStaff.Object);
        
        opType.RemoveRequiredStaff(reqStaff.Object);
        
        Assert.Empty(opType.RequiredStaff);
    }
    
    [Fact]
    public void TestMarkAsNoLongerPerformed()
    {
        var opType = createOperationType();
        
        opType.MarkAsNoLongerPerformed();
        
        Assert.False(opType.stillPerformed);
    }
}