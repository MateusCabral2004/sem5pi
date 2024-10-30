using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.Shared;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.PatientAggregate.Unit;

public class MedicalRecordNumberTest : IValueObject
{

    [Fact]
    public void testMedicalRecordNumberConstructorWithValidParameters()
    {
        var medicalRecordNumber = new MedicalRecordNumber("1234567890");
        Assert.Equal("1234567890", medicalRecordNumber.AsString());
    }
    
    [Fact]
    public void testAsString()
    {
        var medicalRecordNumber = new MedicalRecordNumber("1234567890");
        Assert.Equal("1234567890", medicalRecordNumber.AsString());
    }
    
    [Fact]
    public void testEquals()
    {
        var medicalRecordNumber1 = new MedicalRecordNumber("1234567890");
        var medicalRecordNumber2 = new MedicalRecordNumber("1234567890");
        Assert.True(medicalRecordNumber1.Equals(medicalRecordNumber2));
    }
    
    [Fact]
    public void testEqualsWithDifferentValues()
    {
        var medicalRecordNumber1 = new MedicalRecordNumber("1234567890");
        var medicalRecordNumber2 = new MedicalRecordNumber("0987654321");
        Assert.False(medicalRecordNumber1.Equals(medicalRecordNumber2));
    }
    
    [Fact]
    public void testEqualsWithNullValue()
    {
        var medicalRecordNumber = new MedicalRecordNumber("1234567890");
        Assert.False(medicalRecordNumber.Equals(null));
    }
}