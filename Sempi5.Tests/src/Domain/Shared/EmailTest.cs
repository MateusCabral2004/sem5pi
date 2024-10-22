using System;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace Sempi5.Tests.Domain.Shared;

public class EmailTest
{
    [Fact]
    public void validateEmailAddress_Null()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>new Email(null));
    }

    [Fact]
    public void validateEmailAddress_NoAt()
    {
        Assert.ThrowsException<ArgumentException>(() =>new Email("1220994.isep.ipp.pt"));
    }

    [Fact]
    public void validateEmailAddress_ZeroLength()
    {
        Assert.ThrowsException<ArgumentException>(() => new Email(" "));
    }

    [Fact]
    public void validateEmailAddress_InvalidFormat()
    {
        Assert.ThrowsException<ArgumentException>(()=> new Email("1220994bsdajg"));
    }
    
}