using System;
using JetBrains.Annotations;
using Sempi5.Domain.Shared;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace Sempi5.Tests.Domain.Shared;

[TestClass]
public class EmailTest
{
    [TestMethod]
    public void validateEmailAddress_Null()
    {
        Assert.ThrowsException<ArgumentException>(() =>new Email(null));
    }

    [TestMethod]
    public void validateEmailAddress_NoAt()
    {
        Assert.ThrowsException<ArgumentException>(() =>new Email("1220994.isep.ipp.pt"));
    }

    [TestMethod]
    public void validateEmailAddress_ZeroLength()
    {
        Assert.ThrowsException<ArgumentException>(() => new Email(" "));
    }
    

    
}