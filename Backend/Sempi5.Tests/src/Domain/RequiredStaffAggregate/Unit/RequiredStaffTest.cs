using System;
using Moq;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.SpecializationAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.RequiredStaffAggregate.Unit;

public class RequiredStaffTest
{
    [Fact]
    public void TestConstructorWithValidParameters()
    {
        var numStaff = new Mock<NumberOfStaff>(1);
        var specializationName = new Mock<SpecializationName>("Test");
        var Specialization = new Mock<Specialization>(specializationName.Object);

        var obj = new RequiredStaff(numStaff.Object, Specialization.Object);

        Assert.NotNull(obj);
    }

    [Fact]
    public void TestConstructorWithNullNumberOfStaff()
    {
        var specializationName = new Mock<SpecializationName>("Test");
        var Specialization = new Mock<Specialization>(specializationName.Object);

        Assert.Throws<ArgumentNullException>(() => new RequiredStaff(null, Specialization.Object));
    }

    [Fact]
    public void TestConstructorWithNullSpecialization()
    {
        var numStaff = new Mock<NumberOfStaff>(1);

        Assert.Throws<ArgumentNullException>(() => new RequiredStaff(numStaff.Object, null));
    }

    [Fact]
    public void TestEquals()
    {
        var numStaff1 = new Mock<NumberOfStaff>(1);
        numStaff1.Setup(n => n.Equals(It.IsAny<NumberOfStaff>())).Returns(true);
        
        var specializationName1 = new Mock<SpecializationName>("Test");
        var specialization1 = new Mock<Specialization>(specializationName1.Object);
        specialization1.Setup(s => s.Equals(It.IsAny<Specialization>())).Returns(true);
    
        var numStaff2 = new Mock<NumberOfStaff>(1);
        var specializationName2 = new Mock<SpecializationName>("Test");
        var specialization2 = new Mock<Specialization>(specializationName2.Object);

        var obj1 = new RequiredStaff(numStaff1.Object, specialization1.Object);
        var obj2 = new RequiredStaff(numStaff2.Object, specialization2.Object);

        Assert.True(obj1.Equals(obj2));
    }
    
    [Fact]
    public void TestEqualsWithDifferentNumberOfStaff()
    {
        var numStaff1 = new Mock<NumberOfStaff>(1);
        var numStaff2 = new Mock<NumberOfStaff>(2);
        var specializationName = new Mock<SpecializationName>("Test");
        var specialization = new Mock<Specialization>(specializationName.Object);

        var obj1 = new RequiredStaff(numStaff1.Object, specialization.Object);
        var obj2 = new RequiredStaff(numStaff2.Object, specialization.Object);

        Assert.False(obj1.Equals(obj2));
    }

    [Fact]
    public void TestEqualsWithDifferentSpecialization()
    {
        var numStaff = new Mock<NumberOfStaff>(1);
        var specializationName1 = new Mock<SpecializationName>("Test1");
        var specializationName2 = new Mock<SpecializationName>("Test2");
        var Specialization1 = new Mock<Specialization>(specializationName1.Object);
        var Specialization2 = new Mock<Specialization>(specializationName2.Object);

        var obj1 = new RequiredStaff(numStaff.Object, Specialization1.Object);
        var obj2 = new RequiredStaff(numStaff.Object, Specialization2.Object);

        Assert.False(obj1.Equals(obj2));
    }

    [Fact]
    public void TestEqualsWithNull()
    {
        var numStaff = new Mock<NumberOfStaff>(1);
        var specializationName = new Mock<SpecializationName>("Test");
        var Specialization = new Mock<Specialization>(specializationName.Object);

        var obj1 = new RequiredStaff(numStaff.Object, Specialization.Object);

        Assert.False(obj1.Equals(null));
    }
}