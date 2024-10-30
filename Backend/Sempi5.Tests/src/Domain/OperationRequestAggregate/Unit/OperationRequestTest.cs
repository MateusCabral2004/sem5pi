using System;
using System.Collections.Generic;
using Moq;
using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.User;
using Xunit;

namespace Sempi5.Tests.Domain.OperationRequestAggregate.Unit;

public class OperationRequestTest
{

    private Mock<SystemUser> CreateMockSystemUser()
    {
        var email = new Mock<Email>("rute@gmail.com");
        return new Mock<SystemUser>(email.Object,"roleTest");
    }

    private Mock<Person> CreateMockPerson()
    {
        var firstName = new Mock<Name>("Rute");
        var lastName = new Mock<Name>("Silva");
        var email = new Mock<Email>("rui@teste.com");
        var phoneNumber = new Mock<PhoneNumber>(987654321);
        var contactInfo = new Mock<ContactInfo>(email.Object, phoneNumber.Object);
        return new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
    }
    
    private Mock<Specialization> CreateMockSpecialization()
    {
        var name = new Mock<SpecializationName>("Cardiologia");
        var specialization = new Mock<Specialization>(name.Object);
        return specialization;
    }

    private Mock<Staff> CreateMockStaff()
    {
       var licenseNumber = new Mock<LicenseNumber>(123);
       var person = CreateMockPerson(); 
       var specialization = CreateMockSpecialization();
       
       return new Mock<Staff>(licenseNumber.Object, person.Object, specialization.Object);
    }
    
    private Mock<Patient> CreateMockPatient()
    {
        var user = CreateMockSystemUser();
        var person = CreateMockPerson();
        var patient = new Mock<Patient>(user.Object, person.Object,DateTime.Now,"M",new List<string>(), "123456789", new List<string>());
        return patient;
    }
    
    private Mock<OperationType> CreateMockOperationType()
    {
        var name = new Mock<OperationName>("Cirurgia");
        var operationType = new Mock<OperationType>(name.Object,TimeSpan.FromHours(2),TimeSpan.FromHours(4),TimeSpan.FromHours(6));
        return operationType;
    }
    
    [Fact]
    public void testValidConstructor()
    {
        var doctor = CreateMockStaff();
        var patient = CreateMockPatient();
        var operationType = CreateMockOperationType();
        var deadLineDate = DateTime.Now;
        var priorityEnum = PriorityEnum.HIGH;
        
        var operationRequest = new OperationRequest(doctor.Object, patient.Object, operationType.Object, deadLineDate, priorityEnum);
        
        Assert.NotNull(operationRequest);
    }
    
    [Fact]
    public void testInvalidConstructorWithNullStaff()
    {
        Staff doctor = null;
        var patient = CreateMockPatient();
        var operationType = CreateMockOperationType();
        var deadLineDate = DateTime.Now;
        var priorityEnum = PriorityEnum.HIGH;
        
        Assert.Throws<ArgumentNullException>(() => new OperationRequest(doctor, patient.Object, operationType.Object, deadLineDate, priorityEnum));
    }
    
    [Fact]
    public void testInvalidConstructorWithNullPatient()
    {
        var doctor = CreateMockStaff();
        Patient patient = null;
        var operationType = CreateMockOperationType();
        var deadLineDate = DateTime.Now;
        var priorityEnum = PriorityEnum.HIGH;
        
        Assert.Throws<ArgumentNullException>(() => new OperationRequest(doctor.Object, patient, operationType.Object, deadLineDate, priorityEnum));
    }
    
    [Fact]
    public void testInvalidConstructorWithNullOperationType()
    {
        var doctor = CreateMockStaff();
        var patient = CreateMockPatient();
        OperationType operationType = null;
        var deadLineDate = DateTime.Now;
        var priorityEnum = PriorityEnum.HIGH;
        
        Assert.Throws<ArgumentNullException>(() => new OperationRequest(doctor.Object, patient.Object, operationType, deadLineDate, priorityEnum));
    }
    
}