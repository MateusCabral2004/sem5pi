
using System;
using System.Collections.Generic;
using Moq;
using Sempi5.Domain.AppointmentAggregate;
using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.SurgeryRoomAggregate;
using Sempi5.Domain.User;
using Xunit;

namespace Sempi5.Tests.Domain.AppointmentAggregate.Unit;

public class AppointmentTest
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

    private Mock<OperationRequest> CreateMockOperationRequest()
    {
        
        var doctor = CreateMockStaff();
        var patient = CreateMockPatient();
        var operationType = CreateMockOperationType();
        var deadLineDate = DateTime.Now;
        var priorityEnum = PriorityEnum.HIGH;
        
        return new Mock<OperationRequest>(doctor.Object, patient.Object, operationType.Object, deadLineDate, priorityEnum);
    }

    private Mock<SurgeryRoom> CreateMockSurgeryRoom()
    {
        return new Mock<SurgeryRoom> (RoomTypeEnum.RECOVERY_ROOM, new Mock<RoomCapacity>(2).Object, new List<string>{"equipatmento1", "equipamento2"}, 
            RoomStatusEnum.AVAILABLE, new List<string>{"maintenanceSlots"});
    }
    
    [Fact]
    public void CreateAppointmentWithValidParameters()
    {
        var operationRequest = CreateMockOperationRequest();
        var surgeryRoom = CreateMockSurgeryRoom();
        var date = DateTime.Now;
        var status = StatusEnum.SCHEDULED;
        
        var appointment = new Appointment(operationRequest.Object, surgeryRoom.Object, date, status);
        
        Assert.Equal(operationRequest.Object, appointment.OperationRequest);
        Assert.Equal(surgeryRoom.Object, appointment.SurgeryRoom);
        Assert.Equal(date, appointment.Date);
        Assert.Equal(status, appointment.Status);
    }
    
    [Fact]
    public void CreateAppointmentWithNullOperationRequest()
    {
        var surgeryRoom = CreateMockSurgeryRoom().Object;
        var date = DateTime.Now;
        var status = StatusEnum.SCHEDULED;
        
        Assert.Throws<ArgumentNullException>(() => new Appointment(null, surgeryRoom, date, status));
    }
    
    [Fact]
    public void CreateAppointmentWithNullSurgeryRoom()
    {
        var operationRequest = CreateMockOperationRequest().Object;
        var date = DateTime.Now;
        var status = StatusEnum.SCHEDULED;
        
        Assert.Throws<ArgumentNullException>(() => new Appointment(operationRequest, null, date, status));
    }
    
}