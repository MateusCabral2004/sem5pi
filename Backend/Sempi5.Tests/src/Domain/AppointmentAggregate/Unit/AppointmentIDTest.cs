using Sempi5.Domain.AppointmentAggregate;
using Xunit;
using Assert = Xunit.Assert;


namespace Sempi5.Tests.Domain.AppointmentAggregate.Unit;

public class AppointmentIDTest
{
    [Fact]
    public void ConstructorTest()
    {
        var obj = new AppointmentID(10);

        Assert.NotNull(obj);
        Assert.Equal(10, obj.AsLong());
    }
    
    [Fact]
    public void AsLongTest()
    {
        var obj = new AppointmentID(10);

        Assert.Equal(10, obj.AsLong());
    }
    
    [Fact]
    public void AsStringTest()
    {
        var obj = new AppointmentID(10);

        Assert.Equal("10", obj.AsString());
    }   
}