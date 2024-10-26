using System;
using System.Collections.Generic;
using Moq;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.User;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.StaffAggregate.Unit;

public class StaffTest
{
    [Theory]
    [InlineData(123, "John", "Doe", "teste@gmail.com", 987654321, "Cardiology")]
    [InlineData(2134, "Jane", "Doe", "teste2@gmail.com", 923456780, "Nurse")]
    public void Constructor_ShouldInitializeFieldsCorrectly_WithPersonObject(
        int licenseNumber, string firstName, string lastName, string email, int phoneNumber, string specialization)
    {
        // Arrange
        var mockLicenseNumber = new Mock<LicenseNumber>(licenseNumber);
        var mockSpecializationName = new Mock<SpecializationName>(specialization);
        var mockSpecialization = new Mock<Specialization>(mockSpecializationName.Object);
        var mockFirstName = new Mock<Name>(firstName);
        var mockLastName = new Mock<Name>(lastName);
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockPerson = new Mock<Person>(mockFirstName.Object, mockLastName.Object, mockContactInfo.Object);

        mockLicenseNumber.Setup(l => l.licenseNumber()).Returns(licenseNumber);
        mockSpecializationName.Setup(s => s.name).Returns(specialization);
        mockFirstName.Setup(f => f.ToString()).Returns(firstName);
        mockLastName.Setup(l => l.ToString()).Returns(lastName);
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        mockContactInfo.Setup(c => c.email()).Returns(mockEmail.Object);
        mockContactInfo.Setup(c => c.phoneNumber()).Returns(mockPhoneNumber.Object);
        mockSpecialization.Setup(s => s.specializationName).Returns(mockSpecializationName.Object);
        
        
        // Act
        var staff = new Staff(mockLicenseNumber.Object, mockPerson.Object, mockSpecialization.Object);

        // Assert
        Assert.NotNull(staff);
        Assert.Null(staff.User);
        Assert.Equal(mockLicenseNumber.Object, staff.LicenseNumber);
        Assert.Equal(mockSpecialization.Object, staff.Specialization);
        Assert.Equal(mockPerson.Object, staff.Person);
        Assert.Equal(StaffStatusEnum.INACTIVE, staff.Status);
        Assert.Empty(staff.AvailabilitySlots);
    }

    [Theory]
    [InlineData(123,"John", "Doe", "teste@gmail.com", 987654321)]
    [InlineData(213, "Jane", "Doe", "teste2@gmail.com", 923456780)]
    public void ConstructorWithSpecializationNull(int licenseNumber,string firstName, string lastName, string email, int phoneNumber)
    {
        var mockLicenseNumber = new Mock<LicenseNumber>(licenseNumber);
        var mockFirstName = new Mock<Name>(firstName);
        var mockLastName = new Mock<Name>(lastName);
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockPerson = new Mock<Person>(mockFirstName.Object, mockLastName.Object, mockContactInfo.Object);
        
        Assert.Throws<ArgumentNullException>(() => new Staff(mockLicenseNumber.Object, mockPerson.Object, null));
    }
    
    [Theory]
    [InlineData(123,"Cardiology")]
    [InlineData(213, "Doctor")]
    public void ConstructorWithPersonNull(int licenseNumber, string specialization)
    {
        var mockLicenseNumber = new Mock<LicenseNumber>(licenseNumber);
        var mockSpecializationName = new Mock<SpecializationName>(specialization);
        var mockSpecialization = new Mock<Specialization>(mockSpecializationName.Object);
        
        Assert.Throws<ArgumentNullException>(() => new Staff(mockLicenseNumber.Object, null , mockSpecialization.Object));
    }
    
    [Theory]
    [InlineData("John", "Doe", "teste@gmail.com", 987654321, "Doctor")]
    [InlineData("Jane", "Doe", "teste2@gmail.com", 923456780, " Cardiology")]
    public void ConstructorWithLicenseNumberNull(string firstName, string lastName, string email, int phoneNumber, string specialization)
    {
        var mockFirstName = new Mock<Name>(firstName);
        var mockLastName = new Mock<Name>(lastName);
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockPerson = new Mock<Person>(mockFirstName.Object, mockLastName.Object, mockContactInfo.Object);
        var mockSpecializationName = new Mock<SpecializationName>(specialization);
        var mockSpecialization = new Mock<Specialization>(mockSpecializationName.Object);
        
        Assert.Throws<ArgumentNullException>(() => new Staff(null, mockPerson.Object, mockSpecialization.Object));
    }

    [Fact]
    public void ConstructorWithAllNull()
    {
        
        Assert.Throws<ArgumentNullException>(() => new Staff(null, null, null));
    }

    [Theory]
    [InlineData("teste1@gmail.com","Staff",123, "John", "Doe", "teste@gmail.com", 987654321, "Cardiology")]
    [InlineData("teste2@gmail.com","Staff",2134, "Jane", "Doe", "teste2@gmail.com", 923456780, "Nurse")]
    public void AddUser_ShouldSetUserProperty(
        string emailUser, string role, int licenseNumber, string firstName, string lastName, string email, int phoneNumber, string specialization)
    {
        // Arrange
        var mockEmailUser = new Mock<Email>(emailUser);
        var mockLicenseNumber = new Mock<LicenseNumber>(licenseNumber);
        var mockSpecializationName = new Mock<SpecializationName>(specialization);
        var mockSpecialization = new Mock<Specialization>(mockSpecializationName.Object);
        var mockFirstName = new Mock<Name>(firstName);
        var mockLastName = new Mock<Name>(lastName);
        var mockEmail = new Mock<Email>(email);
        var mockPhoneNumber = new Mock<PhoneNumber>(phoneNumber);
        var mockContactInfo = new Mock<ContactInfo>(mockEmail.Object, mockPhoneNumber.Object);
        var mockPerson = new Mock<Person>(mockFirstName.Object, mockLastName.Object, mockContactInfo.Object);

        mockLicenseNumber.Setup(l => l.licenseNumber()).Returns(licenseNumber);
        mockSpecializationName.Setup(s => s.name).Returns(specialization);
        mockFirstName.Setup(f => f.ToString()).Returns(firstName);
        mockLastName.Setup(l => l.ToString()).Returns(lastName);
        mockEmail.Setup(e => e.ToString()).Returns(email);
        mockPhoneNumber.Setup(p => p.phoneNumber()).Returns(phoneNumber);
        mockContactInfo.Setup(c => c.email()).Returns(mockEmail.Object);
        mockContactInfo.Setup(c => c.phoneNumber()).Returns(mockPhoneNumber.Object);
        mockSpecialization.Setup(s => s.specializationName).Returns(mockSpecializationName.Object);

        var staff = new Staff(mockLicenseNumber.Object, mockPerson.Object, mockSpecialization.Object);
      
        
        var newUser = new Mock<SystemUser>(mockEmailUser.Object, role);

        // Act
        staff.AddUser(newUser.Object);

        // Assert
        Assert.Equal(newUser.Object, staff.User);
    }
    
    
}