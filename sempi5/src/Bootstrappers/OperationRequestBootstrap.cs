using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.OperationRequestAggregate;

namespace Sempi5.Bootstrappers;

public class OperationRequestBootstrap
{
    private readonly IOperationRequestRepository _operationRequestRepository;

    public OperationRequestBootstrap(IOperationRequestRepository operationRequestRepo)
    {
        _operationRequestRepository = operationRequestRepo;
    }

    public async Task SeedOperationRequests()
    {
        var user1 = new SystemUser(new Email("newUser@gmail.com"), "Patient", true);

        var patient1 = new Patient
        (
            user1, 
            new Person(new Name("new"), new Name("User"),
                new ContactInfo(new Email("anotherEmail@gmail.com"), new PhoneNumber(987654322))),
            new DateTime(1990, 1, 10),
            "Female",
            new List<string> { "Peanuts", "Asthma" },
            "456",
            new List<string> { "01/01/2021 9am-10am", "02/02/2021 10am-11am" },
            PatientStatusEnum.ACTIVATED
        );

        var specialization1 = new Specialization(new SpecializationName("Cleaner")
        );

        var specialization2 = new Specialization(new SpecializationName("Heart Surgeon"));

        var requiredStaff1 = new RequiredStaff(new NumberOfStaff(3), specialization1);
        var requiredStaff2 = new RequiredStaff(new NumberOfStaff(1), specialization2);
        
        var time11 = TimeSpan.Parse("03:00:00");
        var time12 = TimeSpan.Parse("02:00:00");
        var time13 = TimeSpan.Parse("01:00:00");

        var operationType1 = new OperationType(new OperationName("Heart Surgery"),
            new List<RequiredStaff> { requiredStaff1, requiredStaff2 }, time11, time12, time13);

        var doctorUser = new SystemUser(new Email("doctor@gmail.com"), "Doctor", true);
        
        var doctor = new Staff
        (
            doctorUser,
            new LicenseNumber(213),
            new Person(new Name("Johnnnnn"), new Name("Doe"), new ContactInfo(new Email("doctor@example.com"), new PhoneNumber(987254321))),
            specialization1,
            StaffStatusEnum.ACTIVE
        );

        var request1 = new OperationRequest(doctor, patient1, operationType1,
            new DateTime(2021, 1, 1), PriorityEnum.HIGH);
        
        await _operationRequestRepository.AddAsync(request1);
    }
}